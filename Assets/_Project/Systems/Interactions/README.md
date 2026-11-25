# Interactions

**Owner:** Group 4 (Objects, Characters and Sound) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Contains: `TouchTrigger`, `RaiseTrigger`, `CompletionFeedback` (glow and sound), and the `TraditionLabel` prefab.

## My plan

- **Person 1 (Charbel):** TouchTrigger and RaiseTrigger (session 2), then the glow and sound when an action completes, the tradition label, and the starter sounds (session 3). The starter sounds are in `Assets/_Project/Audio/Ambience/` and `Audio/SFX/`, listed in `Assets/Docs/credits-and-licences.md`.

## What it does

Scene actions the visitor can do: **touch** an object with either hand, or **raise** an object (for example the lamp in Scene 8). Each one completes only once and can be finished automatically if the visitor does nothing.

Both scripts have the same three shared names (semester1-tasks.md, section 8):

| Name | What it does |
|---|---|
| `OnCompleted` | UnityEvent, called once when the action is done |
| `ShowHint()` | Shows the object's hint (a small hand symbol) |
| `ForceComplete()` | The action happens by itself (the raise object rises smoothly) |

## How to use it

**RaiseTrigger (grab and lift):**

1. Add a grabbable object: **Meta → Tools → Building Blocks → Grab Interaction**. It needs a Rigidbody.
2. Add the `RaiseTrigger` component. `Raise Height` is 0.25 (25 cm) by default.
3. Optional: add a small child object as the hint and drag it into `Hint`. It is hidden at start.
4. In `On Completed`, add the Scene Director and choose `SceneController.InteractionDone`.

**TouchTrigger (touch with a hand):**

1. Give the object a Box, Sphere or Capsule Collider, then add the `TouchTrigger` component.
2. In `Hand Points`, set the size to 2 and drag in the Camera Rig's **LeftHandAnchor** and **RightHandAnchor** (inside `TrackingSpace`).
3. `Touch Distance` is 0.05 (5 cm) by default. Optional: set a `Hint` object.
4. In `On Completed`, add the Scene Director and choose `SceneController.InteractionDone`.

**Glow and sound when an action is done (CompletionFeedback):**

1. On the same object as the trigger, add `CompletionFeedback` and an **Audio Source** (untick Play On Awake, Spatial Blend = 1).
2. Drag the Audio Source into `Audio Source`, and `Audio/SFX/SFX_ActionComplete` into `Sound`.
3. In the trigger's `On Completed`, add the object and choose `CompletionFeedback.Play`.
4. The object's material must be URP Lit (or Meta Lit). Change the colour, strength and duration in the Inspector.

**Devotional tradition label (TraditionLabel):**

1. Drag `Prefabs/TraditionLabel` into the scene, near the object or place the narration talks about, at about eye height. Keep it out of the boundary box.
2. It starts hidden. Call `TraditionLabel.Show` when the narration tells a tradition, and `TraditionLabel.Hide` when it moves on (from a Timeline Signal or any UnityEvent).
3. Tick `Show On Start` if the whole scene is a tradition.

**Testing without a headset:** right-click the component's title in the Inspector during Play and choose **Show Hint** or **Force Complete**.

## Known problems

- `RaiseTrigger` measures height from where the object is when the scene **starts**. If the object falls or settles at the start, place it resting on its surface.
- `ForceComplete()` on a RaiseTrigger turns the Rigidbody kinematic, so the object stays in the air afterwards. That is on purpose (the lamp stays raised).
