EDITOR INSTRUCTIONS — Exporting Final Media (Two MP4s)

Goal: Produce two local MP4 files for Unity StreamingAssets.
  1) I_Can_See_Now_Intro.mp4 — main video with narration + captions burned-in optional + PAH logo (already in Unity overlay ok)
  2) I_Can_See_Now_ASL_Overlay.mp4 — interpreter window on green or black background

A. Main Video (I_Can_See_Now_Intro.mp4)
- Length: ~2:30. Resolution: 1920x1080. Audio: Stereo 48kHz.
- Visuals: follow storyboard; add soft golden particles; cut to station visuals.
- Voice: female, motherly & warm. Pace to match SRT provided.
- Captions: provide SRT sidecar OR burn-in. In Unity we use sidecar; keep font ivory with soft gold shadow.
- Music: soft piano + harp. -7dB under narration.
- PAH logo: bottom-right soft glow (if you prefer to burn-in). In Unity v5 we overlay a live Logo Image already.

B. ASL Overlay (I_Can_See_Now_ASL_Overlay.mp4)
- Record in front of black background. High-contrast clothing. 1080p at 30fps.
- Frame chest-to-shoulders; hands visible at all times.
- Keep signing pace aligned to narration using this cue sheet.
- Export: H.264 .mp4 at 1920x1080, AAC silent (or very low room tone).

Delivery:
- Place both files into Unity `Assets/StreamingAssets/`.
- Put SRT in `Assets/StreamingAssets/Subtitles/I_Can_See_Now_Intro.srt`.
- Test: open PAH_IntroVideo.unity → Play.