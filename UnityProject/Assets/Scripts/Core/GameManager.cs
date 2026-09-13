using UnityEngine;
public class GameManager:MonoBehaviour {
    public StationRouter Router; public QuestSystem Quests; public InventorySystem Inventory;
    void Start(){ if(!Router) Router=GetComponent<StationRouter>(); if(!Quests) Quests=GetComponent<QuestSystem>(); if(!Inventory) Inventory=GetComponent<InventorySystem>(); Router.SwitchToByIndex(0); Quests.SetStation(0); }
    void Update(){
        for(int i=0;i<7;i++) if(Input.GetKeyDown(KeyCode.Alpha1+i)){ Router.SwitchToByIndex(i); Quests.SetStation(i); }
        if(Input.GetKeyDown(KeyCode.F1)) Router.SwitchToByIndex(7);
        if(Input.GetKeyDown(KeyCode.F2)) Router.SwitchToByIndex(8);
        if(Input.GetKeyDown(KeyCode.F3)) Router.SwitchToByIndex(9);
        if(Input.GetKeyDown(KeyCode.F4)) Router.SwitchToByIndex(10);
        if(Input.GetKeyDown(KeyCode.F5)) Router.SwitchToByIndex(11);
        if(Input.GetKeyDown(KeyCode.F6)) Router.SwitchToByIndex(12);
        if(Input.GetKeyDown(KeyCode.B)){ var b=FindObjectOfType<BabyModeManager>(); if(b) b.Toggle(); }
    }
}