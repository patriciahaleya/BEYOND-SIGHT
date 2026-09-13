using UnityEngine; using System.Linq;

public class FaithFriendMomBilingual : MonoBehaviour
{
    public TalkSingClapTTS TTS;
    public TextAsset QAJson;

    [System.Serializable] public class QAItem {
        public string q_en; public string a_en;
        public string q_es; public string a_es;
    }
    [System.Serializable] public class Wrapper { public QAItem[] faq; }
    Wrapper data;

    void Awake(){
        if (!TTS) TTS = FindObjectOfType<TalkSingClapTTS>();
        if (QAJson) data = JsonUtility.FromJson<Wrapper>(QAJson.text);
        if (TTS) TTS.Talk(LanguageManager.IsSpanish ? "Hola, mamá. Estoy aquí para ayudarte." : "Hello, mama. I am here to help.");
    }

    public void Ask(string text)
    {
        if (string.IsNullOrEmpty(text) || data == null || data.faq == null || data.faq.Length == 0) return;
        var code = LanguageManager.Code;
        QAItem best = data.faq[0];
        int bestScore = -1;
        foreach (var item in data.faq)
        {
            string q = code=="es" ? item.q_es.ToLower() : item.q_en.ToLower();
            int score = Score(text.ToLower(), q);
            if (score > bestScore) { bestScore = score; best = item; }
        }
        string ans = code=="es" ? best.a_es : best.a_en;
        TTS?.Talk(ans);
    }

    int Score(string a, string b){
        int s=0; foreach(var w in a.Split(' ')) if(b.Contains(w)) s++; return s;
    }
}
