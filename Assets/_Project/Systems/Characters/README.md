# Characters

**Owner:** Group 4 (Objects, Characters and Sound) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Contains: `Charbel_Placeholder` and `Monk_Placeholder` prefabs, Charbel's animations (idle, walk, kneel and pray), and the `HeadLowered` script. Still to come: a Silhouette prefab.

## My plan

- **Placeholder characters (session 3):** Mixamo characters set to Humanoid, with idle, walk, kneel and pray, and Charbel walking to a point and kneeling using only a Timeline.

## What it does

Placeholder people for the scenes until the real 3D characters are delivered. Charbel moves slowly and calmly with his head lowered, driven only by the scene's Timeline.

| File | What it is |
|---|---|
| `Prefabs/Charbel_Placeholder` | Charbel: dark robe colour, head lowered 15° (`HeadLowered`) |
| `Prefabs/Monk_Placeholder` | A monk: brown robe colour, head lowered 10° |
| `Charbel/Charbel_Idle.fbx` | Breathing idle (loops) |
| `Charbel/Charbel_Walk.fbx` | Short walk forward, 1.63 m (root motion) |
| `Charbel/Charbel_Pray.fbx` | Kneels down from standing, then prays (holds the last pose) |
| `Animation/Placeholder_Idle.controller` | Makes a character stand in idle when no Timeline drives it |

All animations are **Humanoid** and use the avatar from `Charbel_Idle.fbx`, so they work on any Humanoid character.

## How to use it

1. Drag `Charbel_Placeholder` into your scene. It stands in idle by itself.
2. In your scene's Timeline, add an **Animation Track** and drag `Charbel_Placeholder` into its slot.
3. Drag the clips from `Charbel/` onto the track: Idle, then Walk, then Pray. Let the clips **overlap by about 0.3 s** so they blend smoothly.
4. Select the Walk clip and set **Speed Multiplier to 0.8** (slow and calm).
5. So he doesn't snap back to where he started when the next clip begins: right-click the Pray clip and choose **Match Offsets to Previous Clip**.
6. Select the track and set **Track Offsets** to **Apply Transform Offsets**, then set the start Position and Rotation (where he begins walking).

Example: `Sandbox/Charbel/Charbel_WalkKneel.playable` (idle 0 to 2 s, walk 1.7 to 4 s, then kneel and pray). In that example the Pray clip's Position is set by hand to Z 2.07 (the walk's 1.76 m plus the 0.31 m the pray clip starts behind its root).

## Known problems

- The current Mixamo body is a **female** model; swap it for a male character (download the character from Mixamo as FBX for Unity, T-pose, set Rig to Humanoid, and put it in the prefab instead).
- The walk is a short "start walking" clip (1.9 s, 1.63 m). For longer walks, download a looping "Walking" from Mixamo with **In Place unticked**.
- 🟠 If a character slides, floats or snaps back, ask the instructor.
