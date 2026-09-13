using UnityEngine;
public class PrivateModeGuard:MonoBehaviour{
    void Awake(){
#if PRIVATE_BUILD
        Application.targetFrameRate=72; DisableUnityAnalytics(); if(Application.internetReachability!=NetworkReachability.NotReachable){ Debug.LogWarning("[PrivateMode] Network reachable; ensure no web calls."); }
#endif
    }
    void DisableUnityAnalytics(){ var type=System.Type.GetType("UnityEngine.Analytics.Analytics, UnityEngine.Analytics"); if(type!=null){ var prop=type.GetProperty("enabled"); if(prop!=null && prop.CanWrite) prop.SetValue(null,false,null); } }
}