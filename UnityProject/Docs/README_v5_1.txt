I Can See Now — MASTER EDITION v5.1 (Language Picker EN/ES)
Generated: 2025-10-27

What’s new:
- Language Picker (English / Español) with instant app-wide switch
- Language-aware intro video + captions loader
- TTS voice selects proper locale (iOS/Android/PC)
- FaithFriend Mom bilingual Q&A loader

How to use:
1) Add a UI Panel with two buttons and attach LanguagePicker.cs.
2) Drop your media into StreamingAssets with '_en' and '_es' suffixes.
3) In IntroVideoController, captions auto-load matching SRT (en/es).
4) AskMom uses 'AskMom_QA_bilingual.json' and answers in the chosen language.

Files to prepare:
Assets/StreamingAssets/
  I_Can_See_Now_Intro_en.mp4
  I_Can_See_Now_Intro_es.mp4
  Subtitles/I_Can_See_Now_Intro_en.srt
  Subtitles/I_Can_See_Now_Intro_es.srt
