# Rig

**Owner:** Group 1 (The Visitor) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Will contain: The `[VR Rig]` prefab: camera rig and hand tracking, SeatAligner, PlayAreaBoundary and HeadsetPresence.

## My plan

- **Person 1 (Krista):** the `[VR Rig]` prefab and the seat (`SeatAligner`), then the boundary ("Please step back"), then going back to the welcome screen when the headset is taken off (with the instructor).
- **Person 2 (Christy):** Move around: teleport and snap turn (only when `AppState.MovementEnabled` is on), and teleporting only inside the boundary.

## What it does

`[VR Rig]` is the visitor: their eyes (camera) and hands. When a scene starts, it moves the visitor to the scene's `Seat`, looking the way the Seat faces. If the visitor's head leaves a 2 × 2 m box around the Seat, the view goes dark and shows "Please step back".

## How to use it

1. Drag `Prefabs/[VR Rig]` into your scene. Delete any other camera (for example `Main Camera`).
2. Make an empty GameObject and name it exactly **`Seat`**.
3. Put the Seat where the visitor's **eyes** should be: about **1.2 m** above the floor for a seated visitor.
4. Turn the Seat (Rotation Y) so its **blue arrow** (Z, forward) points at what the visitor should look at first.
5. Press Play: the visitor starts at the Seat, facing that way, even if they were sitting turned to the side.

You don't need to set anything on the `Seat Aligner` or `Play Area Boundary` components: they find the object named `Seat` by themselves.

**The boundary box:** select `[VR Rig]` and look at the Scene view: the **green box** around the Seat is the boundary. **Keep every object of your scene outside the green box**, or the visitor could walk into it.

## Known problems

- If the scene has no object named exactly `Seat`, the rig stays where it is and the Console shows a yellow warning: "SeatAligner: no Seat in the scene…".
- The rig is moved once, when the scene starts. If the visitor moves their chair later, they aren't moved back.
- The boundary was tested in Play mode on the PC (by moving the Seat), not yet in the headset.
- Teleport and "headset taken off" aren't built yet.
