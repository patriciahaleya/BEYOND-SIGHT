using UnityEngine; using System.Linq;
public class FaithFriendMomManager:MonoBehaviour{
    public TalkSingClapTTS TTS; public TextAsset QAJson; public TextAsset ResourcesJson; public TextAsset GamesJson;
    [System.Serializable] public class QAItem{ public string q; public string a; } [System.Serializable] public class QAWrapper{ public QAItem[] faq; }
    [System.Serializable] public class Card{ public string title; public string desc; public string action; } [System.Serializable] public class CardWrap{ public Card[] cards; }
    [System.Serializable] public class GameIdea{ public string name; public string how; } [System.Serializable] public class GameWrap{ public GameIdea[] ideas; }
    private QAWrapper _qa; private CardWrap _res; private GameWrap _games;
    void Awake(){ if(!TTS) TTS=FindObjectOfType<TalkSingClapTTS>(); if(QAJson) _qa=JsonUtility.FromJson<QAWrapper>(QAJson.text); if(ResourcesJson) _res=JsonUtility.FromJson<CardWrap>(ResourcesJson.text); if(GamesJson) _games=JsonUtility.FromJson<GameWrap>(GamesJson.text); if(TTS) TTS.Talk("Welcome, sweet mama. Ask me anything."); }
    public void AskQuestion(string text){ if(string.IsNullOrEmpty(text))return; var best=_qa.faq.FirstOrDefault(); int bestScore=-1; foreach(var item in _qa.faq){ int score=SimilarityScore(text.ToLower(), item.q.ToLower()); if(score>bestScore){bestScore=score; best=item;} } TTS?.Talk(best.a); }
    public void ReadResource(int i){ if(_res==null||_res.cards==null||i<0||i>=_res.cards.Length)return; var c=_res.cards[i]; TTS?.Talk(c.title+". "+c.desc+" "+c.action); }
    public void SuggestGame(int i){ if(_games==null||_games.ideas==null||i<0||i>=_games.ideas.Length)return; var g=_games.ideas[i]; TTS?.Talk("Let's try a game. "+g.name+". "+g.how); }
    int SimilarityScore(string a,string b){ int s=0; foreach(var w in a.Split(' ')) if(b.Contains(w)) s++; return s; }
}