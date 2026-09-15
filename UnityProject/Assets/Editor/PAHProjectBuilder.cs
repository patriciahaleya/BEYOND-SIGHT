using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Build.Reporting;
using UnityEngine.SceneManagement;
using System.IO;
using System.Collections.Generic;

public static class PAHProjectBuilder
{
    const string SceneRoot = "Assets/Scenes";

    [MenuItem("PAH/Build Master Scenes")]
    public static void BuildMasterScenes()
    {
        Directory.CreateDirectory(SceneRoot);
        CreateSplash();
        CreateLanguagePicker();
        CreateIntro();
        CreateMain();
        CreateAskMom();

        var scenes = new List<EditorBuildSettingsScene>
        {
            new EditorBuildSettingsScene(SceneRoot + "/Splash_PAH_Rainbow.unity", true),
            new EditorBuildSettingsScene(SceneRoot + "/LanguagePicker.unity", true),
            new EditorBuildSettingsScene(SceneRoot + "/PAH_IntroVideo.unity", true),
            new EditorBuildSettingsScene(SceneRoot + "/Main.unity", true),
            new EditorBuildSettingsScene(SceneRoot + "/AskFaithFriendMom.unity", true)
        };
        EditorBuildSettings.scenes = scenes.ToArray();
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("PAH master scenes created and added to Build Settings.");
    }

    static Canvas MakeCanvas(string name)
    {
        var go = new GameObject(name);
        var canvas = go.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        go.AddComponent<CanvasScaler>().uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        go.AddComponent<GraphicRaycaster>();
        return canvas;
    }

    static Text MakeText(Transform parent, string name, string value, int size, TextAnchor align)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var t = go.AddComponent<Text>();
        t.text = value;
        t.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
        t.fontSize = size;
        t.alignment = align;
        t.color = Color.white;
        var rt = t.rectTransform;
        rt.anchorMin = new Vector2(0.1f, 0.1f);
        rt.anchorMax = new Vector2(0.9f, 0.9f);
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        return t;
    }


    static RawImage MakeArtworkBackground(Transform parent, string name, string resourcePath, Color tint)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        go.transform.SetAsFirstSibling();
        var raw = go.AddComponent<RawImage>();
        raw.texture = Resources.Load<Texture2D>(resourcePath);
        raw.color = tint;
        raw.raycastTarget = false;
        var rt = raw.rectTransform;
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        raw.uvRect = new Rect(0, 0, 1, 1);
        return raw;
    }

    static RawImage MakeArtworkPanel(Transform parent, string name, string resourcePath, Vector2 anchorMin, Vector2 anchorMax)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var raw = go.AddComponent<RawImage>();
        raw.texture = Resources.Load<Texture2D>(resourcePath);
        raw.color = Color.white;
        raw.raycastTarget = false;
        var rt = raw.rectTransform;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        return raw;
    }
    static Button MakeButton(Transform parent, string name, string label, Vector2 anchorMin, Vector2 anchorMax)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent, false);
        var img = go.AddComponent<Image>();
        img.color = new Color(0.36f, 0.15f, 0.60f, 0.95f);
        var btn = go.AddComponent<Button>();
        var rt = img.rectTransform;
        rt.anchorMin = anchorMin;
        rt.anchorMax = anchorMax;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
        MakeText(go.transform, "Label", label, 34, TextAnchor.MiddleCenter);
        return btn;
    }

    static void CreateSplash()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var canvas = MakeCanvas("SplashCanvas");

        var bg = new GameObject("Background");
        bg.transform.SetParent(canvas.transform, false);
        var bgImg = bg.AddComponent<Image>();
        bgImg.color = new Color(0.12f, 0.03f, 0.22f, 1f);
        bgImg.rectTransform.anchorMin = Vector2.zero;
        bgImg.rectTransform.anchorMax = Vector2.one;
        bgImg.rectTransform.offsetMin = Vector2.zero;
        bgImg.rectTransform.offsetMax = Vector2.zero;

        var logoGo = new GameObject("PAH_Heart");
        logoGo.transform.SetParent(canvas.transform, false);
        var logo = logoGo.AddComponent<Image>();
        logo.preserveAspect = true;
        var sprite = Resources.Load<Sprite>("Brand/PAH_Heart");
        if (sprite != null) logo.sprite = sprite;
        var rt = logo.rectTransform;
        rt.anchorMin = new Vector2(0.25f, 0.2f);
        rt.anchorMax = new Vector2(0.75f, 0.8f);
        rt.offsetMin = Vector2.zero; rt.offsetMax = Vector2.zero;

        var ctrl = new GameObject("RainbowGlowController").AddComponent<RainbowGlowSplash>();
        ctrl.LogoImage = logo;
        ctrl.Duration = 5f;

        EditorSceneManager.SaveScene(scene, SceneRoot + "/Splash_PAH_Rainbow.unity");
    }

    static void CreateLanguagePicker()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var canvas = MakeCanvas("LanguageCanvas");
        MakeArtworkBackground(canvas.transform, "LioraArtwork", "Illustrations/Liora_BeautyBeyondSight", new Color(1f,1f,1f,0.42f));
        MakeText(canvas.transform, "Title", "Choose Your Language / Elige tu idioma", 46, TextAnchor.UpperCenter);

        var english = MakeButton(canvas.transform, "EnglishButton", "English", new Vector2(.15f,.35f), new Vector2(.45f,.55f));
        var spanish = MakeButton(canvas.transform, "SpanishButton", "Español", new Vector2(.55f,.35f), new Vector2(.85f,.55f));

        var picker = new GameObject("LanguagePicker").AddComponent<LanguagePicker>();
        picker.EnglishBtn = english;
        picker.SpanishBtn = spanish;

        EditorSceneManager.SaveScene(scene, SceneRoot + "/LanguagePicker.unity");
    }

    static void CreateIntro()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var canvas = MakeCanvas("IntroCanvas");

        var mainRaw = new GameObject("MainVideo");
        mainRaw.transform.SetParent(canvas.transform, false);
        var mainImage = mainRaw.AddComponent<RawImage>();
        mainImage.rectTransform.anchorMin = Vector2.zero;
        mainImage.rectTransform.anchorMax = Vector2.one;
        mainImage.rectTransform.offsetMin = Vector2.zero;
        mainImage.rectTransform.offsetMax = Vector2.zero;

        var mainPlayer = mainRaw.AddComponent<VideoPlayer>();
        mainPlayer.renderMode = VideoRenderMode.APIOnly;

        var aslRaw = new GameObject("ASLWindow");
        aslRaw.transform.SetParent(canvas.transform, false);
        var aslImage = aslRaw.AddComponent<RawImage>();
        aslImage.rectTransform.anchorMin = new Vector2(.02f,.02f);
        aslImage.rectTransform.anchorMax = new Vector2(.28f,.35f);
        aslImage.rectTransform.offsetMin = Vector2.zero;
        aslImage.rectTransform.offsetMax = Vector2.zero;
        var aslPlayer = aslRaw.AddComponent<VideoPlayer>();
        aslPlayer.renderMode = VideoRenderMode.APIOnly;

        var caption = MakeText(canvas.transform, "Captions", "", 34, TextAnchor.LowerCenter);
        caption.rectTransform.anchorMin = new Vector2(.08f,.02f);
        caption.rectTransform.anchorMax = new Vector2(.92f,.25f);

        var watermark = MakeText(canvas.transform, "Watermark", "CONFIDENTIAL — Not for Public Release", 22, TextAnchor.UpperRight);

        var logoGo = new GameObject("LogoOverlay");
        logoGo.transform.SetParent(canvas.transform, false);
        var logo = logoGo.AddComponent<Image>();
        logo.preserveAspect = true;
        var sprite = Resources.Load<Sprite>("Brand/PAH_Heart");
        if (sprite != null) logo.sprite = sprite;
        logo.rectTransform.anchorMin = new Vector2(.82f,.02f);
        logo.rectTransform.anchorMax = new Vector2(.98f,.20f);
        logo.rectTransform.offsetMin = Vector2.zero;
        logo.rectTransform.offsetMax = Vector2.zero;

        var skip = MakeButton(canvas.transform, "SkipButton", "Skip", new Vector2(.84f,.88f), new Vector2(.97f,.97f));

        var captions = new GameObject("CaptionController").AddComponent<SimpleSrtCaptions>();
        captions.CaptionText = caption;

        var ctrl = new GameObject("IntroController").AddComponent<IntroVideoController>();
        ctrl.MainVideo = mainPlayer;
        ctrl.AslVideo = aslPlayer;
        ctrl.MainTarget = mainImage;
        ctrl.AslTarget = aslImage;
        ctrl.Captions = captions;
        ctrl.Watermark = watermark;
        ctrl.LogoOverlay = logo;
        ctrl.SkipButton = skip;
        ctrl.NextScene = "Main";

        EditorSceneManager.SaveScene(scene, SceneRoot + "/PAH_IntroVideo.unity");
    }

    static void CreateMain()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var canvas = MakeCanvas("MainCanvas");
        MakeArtworkBackground(canvas.transform, "BeyondSightArtwork", "Illustrations/BeyondSight_InclusiveFuture", new Color(1f,1f,1f,0.38f));
        MakeText(canvas.transform, "Title", "I Can See Now", 56, TextAnchor.UpperCenter);

        MakeArtworkPanel(canvas.transform, "GlowGardenPanel", "Illustrations/GlowGarden_Adventure", new Vector2(.03f,.74f), new Vector2(.31f,.96f));
        MakeArtworkPanel(canvas.transform, "CalmToolsPanel", "Illustrations/CalmTools_MasterPanel", new Vector2(.35f,.74f), new Vector2(.63f,.96f));
        MakeArtworkPanel(canvas.transform, "ExplorerHubPanel", "Illustrations/Explorer_GameHub", new Vector2(.67f,.74f), new Vector2(.95f,.96f));

        MakeButton(canvas.transform, "HealingStations", "Healing Stations", new Vector2(.10f,.49f), new Vector2(.45f,.64f));
        MakeButton(canvas.transform, "BabyMode", "Baby Mode", new Vector2(.55f,.49f), new Vector2(.90f,.64f));
        MakeButton(canvas.transform, "AskMom", "Ask FaithFriend Mom", new Vector2(.10f,.28f), new Vector2(.45f,.43f));
        MakeButton(canvas.transform, "FamilyHub", "Family Hub", new Vector2(.55f,.28f), new Vector2(.90f,.43f));
        MakeButton(canvas.transform, "Language", "Language / Idioma", new Vector2(.32f,.07f), new Vector2(.68f,.20f));

        new GameObject("GameManager").AddComponent<GameManager>();
        new GameObject("StationRouter").AddComponent<StationRouter>();

        EditorSceneManager.SaveScene(scene, SceneRoot + "/Main.unity");
    }

    static void CreateAskMom()
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
        var canvas = MakeCanvas("AskMomCanvas");
        MakeArtworkBackground(canvas.transform, "LioraArtwork", "Illustrations/Liora_BeautyBeyondSight", new Color(1f,1f,1f,0.28f));
        MakeText(canvas.transform, "Title", "Ask FaithFriend Mom", 50, TextAnchor.UpperCenter);
        MakeText(canvas.transform, "Instructions", "Type or speak your question. Answers follow the selected language.", 30, TextAnchor.MiddleCenter);

        var tts = new GameObject("TTS").AddComponent<TalkSingClapTTS>();
        var mom = new GameObject("FaithFriendMom").AddComponent<FaithFriendMomBilingual>();
        mom.TTS = tts;
        mom.QAJson = Resources.Load<TextAsset>("Config/AskMom_QA_bilingual");

        EditorSceneManager.SaveScene(scene, SceneRoot + "/AskFaithFriendMom.unity");
    }
}
