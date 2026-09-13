using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

public class SimpleSrtCaptions : MonoBehaviour
{
    public Text CaptionText;
    public string ResourcePath = "Subtitles/I_Can_See_Now_Intro_en"; // fallback
    public string StreamingAssetsRelative = "Subtitles/I_Can_See_Now_Intro_en.srt";

    class Line { public double start, end; public string text; }
    List<Line> lines = new List<Line>();
    double t;

    void Start(){ LoadSRT(); }
    void Update(){
        t += Time.deltaTime;
        string show = "";
        foreach (var L in lines){ if (t >= L.start && t <= L.end){ show = L.text; break; } }
        if (CaptionText) CaptionText.text = show;
    }
    public void ResetTimer(){ t = 0; }

    void LoadSRT(){
        string srtText = null;
        string sa = Path.Combine(Application.streamingAssetsPath, StreamingAssetsRelative);
        if (File.Exists(sa)) srtText = File.ReadAllText(sa);
        else {
            TextAsset ta = Resources.Load<TextAsset>(ResourcePath);
            if (ta != null) srtText = ta.text;
        }
        if (string.IsNullOrEmpty(srtText)) return;

        lines.Clear();
        var blocks = Regex.Split(srtText.Trim(), @"\r?\n\r?\n");
        var timeRe = new Regex(@"(\d+):(\d+):(\d+),(\d+)\s*-->\s*(\d+):(\d+):(\d+),(\d+)");
        foreach (var b in blocks){
            var ms = timeRe.Match(b);
            if (!ms.Success) continue;
            double s = ToSec(ms.Groups[1],ms.Groups[2],ms.Groups[3],ms.Groups[4]);
            double e = ToSec(ms.Groups[5],ms.Groups[6],ms.Groups[7],ms.Groups[8]);
            var text = timeRe.Replace(b, "").Trim();
            text = Regex.Replace(text, @"^\d+\s*\r?\n","");
            text = text.Replace("\r","").Replace("\n"," ");
            lines.Add(new Line{ start=s, end=e, text=text });
        }
    }

    double ToSec(Group h, Group m, Group s, Group ms){
        return int.Parse(h.Value)*3600 + int.Parse(m.Value)*60 + int.Parse(s.Value) + int.Parse(ms.Value)/1000.0;
    }
}
