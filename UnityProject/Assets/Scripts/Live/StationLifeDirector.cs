using UnityEngine; using System.Collections.Generic;
public class StationLifeDirector:MonoBehaviour{
    public TalkSingClapTTS Voice; public StoryReader Reader;
    [System.Serializable] public class LiveDef{ public string name,color,accent,glow,talk,sing,clap; public string[] read_lines; }
    [System.Serializable] class Wrap{ public List<LiveDef> stations; }
    private System.Collections.Generic.Dictionary<string,LiveDef> map=new System.Collections.Generic.Dictionary<string,LiveDef>();
    void Awake(){ var ta=Resources.Load<TextAsset>("Config/LiveStations_Master"); var w=JsonUtility.FromJson<Wrap>(ta.text); foreach(var s in w.stations) map[s.name]=s; EventBus.On("station.changed",OnStation); }
    void OnDestroy(){ EventBus.Off("station.changed",OnStation); }
    void OnStation(string name){ if(!map.ContainsKey(name))return; var s=map[name]; if(Camera.main){ Color c; if(ColorUtility.TryParseHtmlString(s.color,out c)) Camera.main.backgroundColor=c; } Voice?.Talk(s.talk); Reader?.LoadLines(s.read_lines); }
    void Update(){ if(Input.GetKeyDown(KeyCode.T)) Voice?.Talk("Hello from TTS."); if(Input.GetKeyDown(KeyCode.S)) Voice?.Sing("Happy Song"); if(Input.GetKeyDown(KeyCode.C)) Voice?.Clap("joy"); if(Input.GetKeyDown(KeyCode.L)) Reader?.ReadNext(); }
}