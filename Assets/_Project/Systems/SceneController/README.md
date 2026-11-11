# SceneController

**Owner:** Group 3 (Story Player) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Will contain: The `[Scene Director]` prefab: plays the scene's Timeline, waits for interactions, goes to the next scene.

## My plan

- **Person 1:** the `[Scene Director]` prefab and `SceneController` (session 2), then the **Wait** signal and `InteractionDone()` (session 3).
- **Person 2:** narration on the Timeline and the steps "How to add narration to a scene" (session 2), then end of chapter and the Narration sound group (session 3).

## What it does

The `[Scene Director]` plays the scene's Timeline when the scene starts, and at the **End Scene** signal it opens the scene named in `nextScene`.

## How to use it

1. Drag `Prefabs/[Scene Director]` into your scene (one per scene).
2. Select it. In **Playable Director**, set **Playable** to your scene's Timeline.
3. In **Scene Controller**, type the next scene's name in **Next Scene** (exactly as in Build Profiles).
4. Open the Timeline (**Window → Sequencing → Timeline**). Add a **Signal Track**, right-click it at the end of your scene, **Add Signal Emitter**, and pick the `EndScene` signal (in `Signals/`).
5. Press Play: the Timeline plays, and at the End Scene signal the next scene opens.

The Signal Track is bound to the `[Scene Director]` automatically when the scene starts, so you can leave its slot empty.

## Known problems

- It uses `SceneManager.LoadScene` for now, so there is no fade. It will switch to Group 2's `SceneFlow.GoTo` once that is merged (`// TODO` in `SceneController.cs`).
- The next scene must be in the Build Profiles scene list, or loading it fails. Only the instructor changes that list on `main`; add scenes to your local list only for testing.
