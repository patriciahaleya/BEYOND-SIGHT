using System; using System.Collections.Generic;
public static class EventBus
{
    private static Dictionary<string, Action<string>> map = new Dictionary<string, Action<string>>();
    public static void On(string evt, Action<string> cb){ if(!map.ContainsKey(evt)) map[evt]=delegate{}; map[evt]+=cb; }
    public static void Off(string evt, Action<string> cb){ if(map.ContainsKey(evt)) map[evt]-=cb; }
    public static void Broadcast(string evt, string payload){ if(map.ContainsKey(evt)) map[evt](payload); }
}
