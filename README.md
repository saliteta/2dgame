# MimiMeowmeow — Police & Thief 2D

A 2D top-down/platformer style **police-and-thief chase game** built in Unity.

## Project Info

- **Engine:** Unity `6000.6.0f1` (Unity 6)
- **Render pipeline:** Universal Render Pipeline (URP) 2D
- **Template:** Unity 2D template
- **Genre:** 2D chase / stealth-arcade (cops vs. thief)

## Getting Started (for collaborators)

1. Install **Unity Hub** if you don't have it: https://unity.com/download
2. In Unity Hub, install the exact editor version **6000.6.0f1** (Unity 6). Using a different version can cause the project to re-import or break.
3. Clone the repo:
   ```
   git clone <repo-url>
   cd upr2d
   ```
4. This project uses **Git LFS** for binary assets (images, audio, fonts, etc.). Install it once per machine, then pull LFS files:
   ```
   git lfs install
   git lfs pull
   ```
5. Open Unity Hub → **Add** → select the `upr2d` folder (the one containing `Assets/`, `ProjectSettings/`, `Packages/`).
6. Let Unity import the project (this regenerates `Library/`, `Temp/`, `obj/`, etc. — these are intentionally not tracked in git).
7. Open the `Assets/Scenes/SampleScene.unity` scene to start.

> Note: `Library/`, `Temp/`, `Logs/`, `obj/`, `UserSettings/`, and generated `.csproj`/`.sln` files are excluded from git — Unity regenerates them automatically on open/import. Don't commit them.

## Project Structure

```
Assets/
  prefabs/     - Reusable game object prefabs
  Scenes/      - Unity scenes (SampleScene is the main scene)
  scripts/     - Gameplay C# scripts (PlayerControl, policeWayPoints, ...)
  Settings/    - URP / render pipeline settings
  Welcome/     - Default assets from the Unity 2D template
```

## Current Status / TO DO

- [x] Basic player control
- [x] Police waypoint / patrol logic
- [ ] **Shadow processing** — lighting/shadows are not implemented yet (currently the main gap)
- [ ] (add further TODOs here as the project progresses)

## Collaboration Notes

- Unity `.scene`, `.prefab`, and `.asset` files are YAML-based and can be merged, but conflicts are still possible when multiple people edit the same scene/prefab. Prefer working in separate scenes/prefabs where possible, and communicate before editing shared scenes.
- Keep the Unity **Editor version in sync** across the team (see Project Info above) to avoid unnecessary asset re-serialization diffs.
- Meta files (`*.meta`) are tracked and must stay next to their corresponding assets — do not delete or regenerate them manually.
