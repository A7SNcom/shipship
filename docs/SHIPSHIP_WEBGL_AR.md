# معاينة لعبة شب شب — Unity WebGL

المشروع نسخة من Chaos Arena، ويعمل في Unity **2022.3.47f1**. المعاينة تستخدم مشهدي المشروع الأصليين كما هما:

1. `Assets/_Components/Scenes/CardDevScene.unity` — اختيار البطاقات.
2. `Assets/_Components/Scenes/CharDevScene.unity` — المعركة.

هذه مشاهد تطوير أصلية؛ لا يوجد تأكيد مسبق أن اللعبة مكتملة أو خالية من الأخطاء.

## الأسهل: المعاينة على جهاز Windows بدون خادم

1. نزّل **Unity Hub** من الموقع الرسمي.
2. من **Installs** ثبّت Unity **2022.3.47f1**، وحدد إضافة **WebGL Build Support**.
3. نزّل المشروع من [GitHub](https://github.com/A7SNcom/shipship) باستخدام **Code → Download ZIP** ثم فك الضغط، أو استنسخه عبر Git.
4. من Unity Hub اختر **Add project from disk** وحدد مجلد اللعبة الذي يحوي `Assets` و`Packages` و`ProjectSettings`.
5. بعد فتحه، من قائمة Unity العليا اختر **ShipShip → Build and Run WebGL Preview**.
6. سيبني Unity اللعبة ويفتحها في متصفحك محلياً. النسخة الناتجة تُحفظ في `build/WebGL/WebGL`.

إذا لم تظهر قائمة ShipShip أو ظهرت أخطاء حمراء، افتح Console في Unity؛ ربما توجد أخطاء أصلية في المشروع أو أصول مفقودة.

## البناء من GitHub (يحتاج إعداد حساب Unity مرة واحدة)

**لا ترسل بيانات حساب Unity أو ملف الترخيص عبر المحادثة، ولا تضعها داخل المستودع.**

1. سجّل دخولك في Unity Hub على جهازك، وفعّل رخصة Unity Personal من **Preferences → Licenses → Add → Get a free personal license** (أو رخصتك المدفوعة وفق GameCI).
2. على Windows: ملف الترخيص غالباً في `C:\ProgramData\Unity\Unity_lic.ulf`.
3. افتح [إعدادات الأسرار للمستودع](https://github.com/A7SNcom/shipship/settings/secrets/actions) واضغط **New repository secret** ثلاث مرات، وأضف:
   - `UNITY_LICENSE` — النص الكامل لملف `.ulf`.
   - `UNITY_EMAIL` — بريد حساب Unity.
   - `UNITY_PASSWORD` — كلمة مرور حساب Unity.
4. افتح [صفحة GitHub Actions](https://github.com/A7SNcom/shipship/actions). إن كانت Actions معطّلة على النسخة المتفرعة، فعّلها.
5. اختر **ShipShip Unity WebGL Preview** → **Run workflow**. في التجربة الأولى اترك `publish_pages` غير مفعّل.
6. بعد نجاح المهمة، من صفحة التشغيل تحت **Artifacts** نزّل `shipship-unity-webgl-preview`.

نستخدم `game-ci/unity-builder@v4` لبناء Unity في GitHub، مع حفظ ملفات `Library` لتسريع البنيات التالية. التعليمة البرمجية للبناء موجودة في `Assets/Editor/ShipShipWebGLBuild.cs` وتعطّل ضغط Unity WebGL مسبقاً للتوافق مع استضافة GitHub Pages التي لا نتحكم في ترويساتها.

## رابط المعاينة العامة — بعد التحقق من الحقوق

1. قبل رفع نسخة اللعبة علناً، احصل على إذن مناسب من أصحاب **كود Chaos Arena** والأصول/المجسمات والمكتبات الخارجية. وجود مشروع عام على GitHub أو القدرة على عمل Fork لا يعني حصولك على حق إعادة نشر اللعبة؛ ولم نجد ترخيصاً صريحاً للمستودع الأصلي.
2. في [إعدادات Pages](https://github.com/A7SNcom/shipship/settings/pages) اجعل **Source = GitHub Actions**.
3. شغّل **ShipShip Unity WebGL Preview** من Actions مرة ثانية مع تفعيل مربع `publish_pages`.
4. بعد نجاح وظيفتي **Build Unity WebGL** و **Publish approved browser preview**، افتح:
   **https://a7sncom.github.io/shipship/**

> النشر العام **لا يعمل تلقائياً** حتى تفعّل `publish_pages` يدوياً. السبب: المحافظة على الملفات والحقوق لحين التأكد من صلاحية إعادة توزيعها.

## إصلاحات شائعة

- **Missing Unity secrets**: لم تُضف الأسرار الثلاثة أو أسماؤها غير صحيحة.
- **Activation error**: ملف رخصة Unity أو الحساب غير صالح للاستخدام داخل GameCI. راجع [توثيق التفعيل الرسمي](https://game.ci/docs/github/activation/).
- **WebGL module missing**: ثبّت **WebGL Build Support** لنفس إصدار Unity.
- **Unity compiler errors**: راجع الأخطاء البرمجية الأصلية في Console/Actions؛ لا يمكن تصحيحها بالاستضافة وحدها.
- **صفحة 404**: لا تظهر اللعبة حتى ينجح النشر وتُفعّل Pages على GitHub Actions.
- **Build too large**: راجع حد حجم GitHub Pages، أو انقل النسخة إلى استضافة أخرى.
- **أوفلاين**: بعد تنزيل نسخة اللعبة يمكنك اختبارها محلياً من Unity باستخدام Build and Run؛ مجرد فتح `index.html` عبر `file://` لا يعمل عادةً بسبب قيود WebGL.

## روابط مهمة

- [سجل البناء والنشر](https://github.com/A7SNcom/shipship/actions/workflows/shipship-unity-webgl.yml)
- [ترخيص Unity / التفعيل عبر GameCI](https://game.ci/docs/github/activation/)
- [رابط المعاينة المستقبلي](https://a7sncom.github.io/shipship/)
