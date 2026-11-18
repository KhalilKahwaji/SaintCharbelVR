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

## How to add narration to a scene

Every scene's narration and background sound live on its Timeline. Your scene must already have a `[Scene Director]` with a Timeline (see above).

1. **Put your files in place.** Narration goes in `Assets/_Project/Audio/VO/<scene>/`, one file per line, named like `VO_C2_S08_01.wav` (chapter 2, scene 8, line 1).
2. **Set the import settings.** Select all your narration files in the Project window. In the Inspector, tick **Force To Mono**, set **Load Type** to **Compressed In Memory** (never **Streaming**, it can start late on the Quest), and click **Apply**.
3. **Open the Timeline.** Select the `[Scene Director]` and open **Window → Sequencing → Timeline**.
4. **Add the narration track.** Click **+** → **Audio Track**, then rename it `Narration`. Leave its object slot empty.
5. **Add the lines in order.** Drag `_01` onto the Narration track at 0 s. Drag `_02` so it starts about **0.5 s** after `_01` ends, then `_03` the same way, and so on. The clips must not overlap.
6. **Add the background sound.** Click **+** → **Audio Track**, rename it `Ambience`, and drag the background sound onto it at 0 s. Drag the clip's right edge until it reaches the end of your scene. Select the clip and tick **Loop** in the Inspector if the sound is shorter than the scene.
7. **Make the background quieter.** Select the `Ambience` track (its name on the left) and set **Volume** to about `0.3` in the Inspector, so the voice is easy to hear.
8. **End the scene after the last line.** Drag the **End Scene** signal on the Signal Track to about **1 s after the last narration clip ends**.
9. **Test.** Press Play. You hear the lines in order over the background, and the next scene opens after the last line.

The example is `NarrationTest/Narration_Test.unity`: 3 lines (`VO_Test_01` to `_03`, made with Windows text-to-speech) over a generated wind loop (`Amb_Test_Wind`).

## Known problems

- It uses `SceneManager.LoadScene` for now, so there is no fade. It will switch to Group 2's `SceneFlow.GoTo` once that is merged (`// TODO` in `SceneController.cs`).
- The next scene must be in the Build Profiles scene list, or loading it fails. Only the instructor changes that list on `main`; add scenes to your local list only for testing.
- The Narration and Ambience tracks have no slot set yet, so they play straight to the headset. When Group 4's `MainMixer` is merged, they will be connected to its Narration and Ambience groups so the music gets quieter under the voice (session 3).
