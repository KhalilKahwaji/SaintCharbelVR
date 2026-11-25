# Gaze

**Owner:** Group 2 (Screens and Navigation) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Will contain: GazePointer, GazeTarget and GazeTrigger, plus the `01_Menu` scene.

## My plan

- Person 1: Christy (fade and load, `AppState`, welcome screen, credits). Her work is in `SceneFlow/`.
- Person 2: Rita (this folder).
- Session 2: select by looking: `GazePointer` (dot, ring, 2-second timer) and `GazeTarget` (the `OnSelected` event). **Done, not yet tested in the headset.**
- Session 3: the chapter menu (`01_Menu`) and `GazeTrigger`, a "look at this for 2 seconds" scene action.

## What it does

The visitor selects an object by looking at it: a small dot shows where they look, a ring fills up over 2 seconds, and then the object's `OnSelected` event fires.

## How to use it

1. **The pointer (once per scene).** Under the camera rig's `CenterEyeAnchor`, add the `GazeReticle` canvas (World Space, Pos `0, 0, 1`, Scale `0.001`) with two Images: `Ring` (Knob sprite, Image Type **Filled**, Radial 360, Fill Amount `0`) and `Dot` below it. See `Gaze_Test.unity` for a working example.
2. Add the **Gaze Pointer** component to `CenterEyeAnchor` and drag `Ring` into its **Ring** field.
3. **Each selectable object:** make sure it has a **Collider** (cubes and planes have one already), then add the **Gaze Target** component.
4. In **Gaze Target → On Selected ()**, click **+** and choose what happens, like a button's On Click.
5. Optional: change **Select Time** (seconds, default 2) or **Max Distance** (metres, default 10) on Gaze Pointer.

**Test scene:** `Gaze_Test.unity`. Press Play: after 2 seconds the Console shows `Selected: TargetCube`. `OtherCube` has no Gaze Target, so it never gets selected; move it in front of TargetCube during Play and the ring empties.

## Known problems

- **Not tested in the headset yet** (only in Play mode in the Editor).
- The ring fills like a pie, because it uses Unity's built-in round `Knob` sprite. A real ring image can replace it later; only the Source Image needs changing.
- The pointer is 1 m in front of the eyes, so an object closer than 1 m covers the dot.
- Every scene needs the pointer on its camera. The camera is inside Group 1's `[VR Rig]` prefab, so the reticle should become a prefab that Group 1 adds to the rig. Not decided yet.
- Only objects with a **Collider** can be selected. UI buttons have no Collider, so the chapter menu cards will need one.
