using UnityEngine; using System.Collections;
public class JoyPracticeStation:MonoBehaviour{ public Light Ambient; public float Base=1.1f; void OnEnable(){ StartCoroutine(Session()); }
System.Collections.IEnumerator Session(){ for(int n=0;n<3;n++){ float t=0,d=6; while(t<d){ t+=Time.deltaTime; if(Ambient) Ambient.intensity=Base+Mathf.Sin(t/d*Mathf.PI); yield return null; } } } }