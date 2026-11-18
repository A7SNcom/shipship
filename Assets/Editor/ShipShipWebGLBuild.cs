// ShipShip: prepare a playable WebGL preview from the original Unity project.
// The two enabled scenes are the card selection flow followed by the battle scene.
// Build locally from Unity's ShipShip menu, or from GameCI using Build() below.
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class ShipShipWebGLBuild
{
    private static readonly string[] Scenes =
    {
        "Assets/_Components/Scenes/CardDevScene.unity",
        "Assets/_Components/Scenes/CharDevScene.unity"
    };

    [MenuItem("ShipShip/Build WebGL Preview")]
    public static void Build()
    {
        BuildInternal(false);
    }

    [MenuItem("ShipShip/Build and Run WebGL Preview")]
    public static void BuildAndRun()
    {
        BuildInternal(true);
    }

    private static void BuildInternal(bool openBrowser)
    {
        foreach (string scene in Scenes)
        {
            if (AssetDatabase.LoadAssetAtPath<SceneAsset>(scene) == null)
                throw new InvalidOperationException("ShipShip: missing Unity scene: " + scene);
        }

        if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
        {
            if (!EditorUserBuildSettings.SwitchActiveBuildTarget(
                BuildTargetGroup.WebGL, BuildTarget.WebGL))
            {
                throw new InvalidOperationException(
                    "ShipShip: WebGL build support is missing. Install it in Unity Hub.");
            }
        }

        // GitHub Pages cannot supply Content-Encoding for Unity's precompressed
        // *.unityweb output. Uncompressed .js/.wasm files avoid that failure.
        // This setting is applied only during the build process, not committed.
        PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
        PlayerSettings.WebGL.decompressionFallback = false;

        string projectRoot = Directory.GetParent(Application.dataPath).FullName;
        string output = Path.Combine(projectRoot, "build", "WebGL", "WebGL");
        Directory.CreateDirectory(output);

        Debug.Log("ShipShip: building WebGL into " + output);
        BuildReport report = BuildPipeline.BuildPlayer(new BuildPlayerOptions
        {
            scenes = Scenes,
            locationPathName = output,
            target = BuildTarget.WebGL,
            options = openBrowser ? BuildOptions.AutoRunPlayer : BuildOptions.None
        });

        if (report.summary.result != BuildResult.Succeeded)
            throw new InvalidOperationException(
                "ShipShip WebGL build failed: " + report.summary.result +
                " (" + report.summary.totalErrors + " errors)");

        string index = Path.Combine(output, "index.html");
        if (!File.Exists(index))
            throw new FileNotFoundException("ShipShip WebGL build has no index.html", index);

        Debug.Log("ShipShip WebGL preview successfully built at: " + index);
    }
}
