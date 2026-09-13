# I Can See Now — MASTER EDITION v4 (PAH • Private Build)
Generated: 2025-10-27

This is a **single Unity 2022.3 LTS project** containing everything your developer needs:

- Healing Stations Hub + Family Stations + Baby Mode
- Ask FaithFriend Mom (Q&A for parents, resources, home games)
- On-device TTS (Apple/Android/PC)
- Auto-Play **Intro Video** scene (first launch only) with **captions + ASL window**
- Privacy-first settings (offline; no analytics)

## Open & Run
1) Open `UnityProject/` in **Unity 2022.3 LTS**.
2) Place the final media in `Assets/StreamingAssets/`:
   - `I_Can_See_Now_Intro.mp4` (motherly & warm narration, soft piano/harp, PAH logo bottom-right)
   - `I_Can_See_Now_ASL_Overlay.mp4` (ASL interpreter PiP)
   - `Subtitles/I_Can_See_Now_Intro.srt` (timed captions) — sample included
3) Open `Assets/Scenes/PAH_IntroVideo.unity` and press **Play** to test.
4) After playback the app transitions to `Main.unity` (Healing Hub). On later launches, intro is skipped.

## Private Build Checklist
- Add **PRIVATE_BUILD** to Scripting Define Symbols.
- Disable Unity Analytics/Crash Upload.
- Keep all networking off (Android: remove INTERNET permission if any plugin adds it; iOS: no ATS exceptions).

See `Docs/QuickStart_v4.txt` for setup, and `Docs/Build_Profiles.txt` for platform builds.
