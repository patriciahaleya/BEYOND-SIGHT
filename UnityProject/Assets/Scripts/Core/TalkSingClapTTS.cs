using UnityEngine; using System.Runtime.InteropServices; using System.Diagnostics;

public class TalkSingClapTTS : MonoBehaviour
{
    public void Talk(string text){ Speak(text); }
    public void Sing(string text){ Speak("♪ " + text); }
    public void Clap(string _){ Speak("👏 Clap clap for you!"); }

    public void Speak(string text)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        var up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        var act = up.GetStatic<AndroidJavaObject>("currentActivity");
        var tts = new AndroidJavaObject("android.speech.tts.TextToSpeech", act, null);
        var lang = new AndroidJavaObject("java.util.Locale",
            LanguageManager.IsSpanish ? "es" : "en",
            LanguageManager.IsSpanish ? "MX" : "US");
        tts.Call<int>("setLanguage", lang);
        tts.Call<int>("speak", text, 0, null, "utteranceId");
#elif UNITY_IOS && !UNITY_EDITOR
        _SpeakIOS(text, LanguageManager.TTSLocaleIOS);
#elif UNITY_STANDALONE_OSX && !UNITY_EDITOR
        Run("/usr/bin/say", (LanguageManager.IsSpanish ? "-v Paulina " : "-v Samantha ") + Quote(text));
#elif UNITY_STANDALONE_WIN && !UNITY_EDITOR
        string ps=$@"Add-Type -AssemblyName System.Speech;
        $s=New-Object System.Speech.Synthesis.SpeechSynthesizer;
        try {{ $s.SelectVoiceByHints([System.Speech.Synthesis.VoiceGender]::Female, [System.Speech.Synthesis.VoiceAge]::Adult, 0, [System.Globalization.CultureInfo]::GetCultureInfo('{(LanguageManager.IsSpanish?"es-ES":"en-US")}')); }} catch {{ }}
        $s.Speak('{text.Replace("'", "''")}');";
        Run("powershell.exe","-NoProfile -Command "+Quote(ps));
#else
        Debug.Log($"[TTS Simulated {LanguageManager.Code}] {text}");
#endif
    }

#if UNITY_IOS && !UNITY_EDITOR
    [DllImport("__Internal")] private static extern void _SpeakIOS(string message,string lang);
#endif
#if UNITY_STANDALONE && !UNITY_EDITOR
    private static void Run(string file,string args){ try{ var p=new Process(); p.StartInfo.FileName=file; p.StartInfo.Arguments=args; p.StartInfo.CreateNoWindow=true; p.Start(); } catch(System.Exception e){ UnityEngine.Debug.LogWarning(e.Message);} }
    private static string Quote(string s)=>"\""+s.Replace("\"","\\\"")+"\"";
#endif
}
