using UnityEngine; using System.Collections.Generic;
public class StationRouter:MonoBehaviour
{
    public AudioSource Ambience; public HapticsEngine Haptics; public AromaController Aroma; private List<StationDef> defs;
    void Awake(){ defs = StationDef.LoadAll(); }
    public void SwitchToByIndex(int idx){
        if(defs==null||defs.Count==0) defs=StationDef.LoadAll();
        if(idx<0||idx>=defs.Count) return;
        var d = defs[idx];
        if(Camera.main){ Color c; if(ColorUtility.TryParseHtmlString(d.color, out c)) Camera.main.backgroundColor=c; }
        Aroma?.Emit(d.aroma, 0.6f); Haptics?.PlayPattern("enter"); Ambience?.Play();
        EventBus.Broadcast("station.changed", d.name);
    }
}
[System.Serializable] public class StationDef {
    public int id; public string name; public string color; public string aroma; public string haptics; public string music; public string[] quests;
    public static List<StationDef> LoadAll(){ var ta=Resources.Load<TextAsset>("Config/Stations_Master"); Wrapper w=JsonUtility.FromJson<Wrapper>(ta.text); return w.stations; }
    public Color Color(){ Color c; if(ColorUtility.TryParseHtmlString(color, out c)) return c; return Color.black; }
    [System.Serializable] class Wrapper{ public List<StationDef> stations; }
}