using UnityEngine; using System.Collections.Generic;
public class QuestSystem:MonoBehaviour{
    private int stationIndex=0; private int step=0; private List<StationDef> defs;
    void Awake(){ defs = StationDef.LoadAll(); }
    public void SetStation(int idx){ stationIndex=idx; step=0; EventBus.Broadcast("quest.goals", defs[idx].name); }
    public void CompleteStep(){ var d=defs[stationIndex]; step++; if(step>=d.quests.Length){ FindObjectOfType<InventorySystem>()?.AddLightSeed(1); step=0; EventBus.Broadcast("quest.complete", d.name);} }
}