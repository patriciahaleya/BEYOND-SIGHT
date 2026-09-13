using UnityEngine;

public static class LanguageManager
{
    // "en" (default) or "es"
    public static string Code
    {
        get => PlayerPrefs.GetString("pah.lang", "en");
        set { PlayerPrefs.SetString("pah.lang", value); PlayerPrefs.Save(); }
    }

    public static bool IsEnglish => Code == "en";
    public static bool IsSpanish => Code == "es";

    // StreamingAssets filenames
    public static string IntroVideoFile => $"I_Can_See_Now_Intro_{Code}.mp4";
    public static string IntroSrtRelative => $"Subtitles/I_Can_See_Now_Intro_{Code}.srt";

    // TTS locales (platform-dependent; pick common voices/locales)
    public static string TTSLocaleIOS   => IsSpanish ? "es-MX" : "en-US";
    public static string TTSLocaleDroid => IsSpanish ? "es_MX" : "en_US";
    public static string TTSLocaleWin   => IsSpanish ? "es-ES" : "en-US";
}
