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
  scripts/     - Gameplay C# scripts (PlayerMovement, EnemyPatrol, PoliceVision, die, WinZone, ShadowSetup, ...)
  Settings/    - URP / render pipeline settings
  Welcome/     - Default assets from the Unity 2D template
```

## Current Status / TO DO

- [x] Basic player control (`PlayerMovement.cs`)
- [x] Police waypoint / patrol logic (`EnemyPatrol.cs`)
- [x] **Police light detection** — `PoliceVision.cs` kills the player when they are inside a police Spot Light 2D cone (uses the light's radius/angle) and not hidden behind a wall (any `Collider2D`).
- [x] **Player death / game-over handling** — `die.cs` stops the player, freezes the game, shows GAME OVER; press R to restart the level.
- [x] **Win condition / next level** — `WinZone.cs` (used by the `wayOut` prefab): reaching the exit area loads the next scene in the build list; the last level shows a final win screen (R to play again).
- [x] Prefabs: `police`, `player`, `wayOut`, `wall`
- [ ] **Shadow processing** — in progress. `ShadowSetup.cs` adds `ShadowCaster2D` to assigned blockers and enables shadows on a `Light2D`; currently only one blocker is assigned in `SampleScene` (the walls don't cast shadows yet) and its `spotLight` is empty. Global (ambient) light interaction still needs tuning/testing.
- [ ] **Move `PoliceVision` onto the `police` prefab** — it is currently only added to the police instance in `SampleScene`, so new police copies won't detect the player.
- [ ] **More levels** — only `SampleScene` exists; add level scenes (each with a `wayOut`) to the build scene list in order.
- [ ] **Automatic level generator** — generate levels procedurally: place walls, the player start and the `wayOut` exit, and spawn police with patrol waypoints/rotations, making sure a path to the exit exists that can be taken without being caught (e.g. difficulty scaling with level number).
- [ ] (add further TODOs here as the project progresses)

## Collaboration Notes

- Unity `.scene`, `.prefab`, and `.asset` files are YAML-based and can be merged, but conflicts are still possible when multiple people edit the same scene/prefab. Prefer working in separate scenes/prefabs where possible, and communicate before editing shared scenes.
- Keep the Unity **Editor version in sync** across the team (see Project Info above) to avoid unnecessary asset re-serialization diffs.
- Meta files (`*.meta`) are tracked and must stay next to their corresponding assets — do not delete or regenerate them manually.
