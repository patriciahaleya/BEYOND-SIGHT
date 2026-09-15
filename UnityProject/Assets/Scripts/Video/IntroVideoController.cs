using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideoController : MonoBehaviour
{
    public VideoPlayer MainVideo;
    public VideoPlayer AslVideo;
    public RawImage MainTarget;
    public RawImage AslTarget;
    public SimpleSrtCaptions Captions;
    public Text Watermark;
    public bool ShowWatermark = true;
    public Image LogoOverlay;
    public Button SkipButton;
    public string NextScene = "Main";
    public string FallbackArtworkResource = "Illustrations/BeyondSight_InclusiveFuture";
    public float FallbackDuration = 6f;

    void Start()
    {
        if (ShowWatermark && Watermark) Watermark.text = "CONFIDENTIAL — Not for Public Release";
        if (SkipButton) SkipButton.onClick.AddListener(EndIntro);
        LoadAndPlay();
    }

    void LoadAndPlay()
    {
        string sa = Application.streamingAssetsPath;
        string mainPath = System.IO.Path.Combine(sa, LanguageManager.IntroVideoFile);
        string aslPath  = System.IO.Path.Combine(sa, "I_Can_See_Now_ASL_Overlay.mp4");

        if (System.IO.File.Exists(mainPath))
        {
            MainVideo.url = mainPath;
            MainVideo.isLooping = false;
            MainVideo.prepareCompleted += (v)=>{ v.Play(); if(Captions!=null){ Captions.StreamingAssetsRelative = LanguageManager.IntroSrtRelative; Captions.ResetTimer(); }};
            MainVideo.loopPointReached += (v)=> EndIntro();
            MainVideo.Prepare();
        }
        else
        {
            if (MainTarget)
            {
                MainTarget.texture = Resources.Load<Texture2D>(FallbackArtworkResource);
                MainTarget.color = Color.white;
            }
            if (Captions != null)
            {
                Captions.StreamingAssetsRelative = LanguageManager.IntroSrtRelative;
                Captions.ResetTimer();
            }
            StartCoroutine(EndAfterFallback());
        }

        if (System.IO.File.Exists(aslPath))
        {
            if (AslTarget) AslTarget.gameObject.SetActive(true);
            AslVideo.url = aslPath;
            AslVideo.isLooping = true;
            AslVideo.prepareCompleted += (v)=> v.Play();
            AslVideo.Prepare();
        }
        else if (AslTarget)
        {
            AslTarget.gameObject.SetActive(false);
        }

        if (LogoOverlay) LogoOverlay.enabled = true;
    }

    System.Collections.IEnumerator EndAfterFallback()
    {
        yield return new WaitForSeconds(FallbackDuration);
        EndIntro();
    }

    public void EndIntro()
    {
        PlayerPrefs.SetInt("pah.intro.seen", 1);
        SceneManager.LoadScene(NextScene);
    }
}
