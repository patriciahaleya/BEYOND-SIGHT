using UnityEngine;
public class MomUILines:MonoBehaviour{ public FaithFriendMomManager Mom; string text="";
    void OnGUI(){ GUILayout.BeginArea(new Rect(10,10,520,200), GUI.skin.box); GUILayout.Label("Ask FaithFriend Mom (type + Enter):"); GUI.SetNextControlName("askbox"); text=GUILayout.TextField(text, 200);
    if(Event.current.isKey && Event.current.keyCode==KeyCode.Return){ Mom?.AskQuestion(text); text=""; GUI.FocusControl("askbox"); }
    if(GUILayout.Button("Suggest a Home Game")) Mom?.SuggestGame(Random.Range(0,5)); if(GUILayout.Button("Read a Resource")) Mom?.ReadResource(Random.Range(0,4)); GUILayout.EndArea(); } }