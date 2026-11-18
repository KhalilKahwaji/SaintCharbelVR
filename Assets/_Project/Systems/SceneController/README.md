# SceneController

**Owner:** Group 3 (Story Player) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Will contain: The `[Scene Director]` prefab: plays the scene's Timeline, waits for interactions, goes to the next scene.

## My plan

- **Person 1:** the `[Scene Director]` prefab and `SceneController` (session 2), then the **Wait** signal and `InteractionDone()` (session 3).
- **Person 2:** narration on the Timeline and the steps "How to add narration to a scene" (session 2), then end of chapter and the Narration sound group (session 3).

## What it does

The `[Scene Director]` plays the scene's Timeline when the scene starts, pauses it at each **Wait** signal until the visitor does the action (or 20 seconds pass), and at the **End Scene** signal opens the next scene, or the chapter menu after the last scene of a chapter.

## How to use it

1. Drag `Prefabs/[Scene Director]` into your scene (one per scene).
2. Select it. In **Playable Director**, set **Playable** to your scene's Timeline.
3. In **Scene Controller**, type the next scene's name in **Next Scene** (exactly as in Build Profiles).
4. Open the Timeline (**Window → Sequencing → Timeline**). Add a **Signal Track**, right-click it at the end of your scene, **Add Signal Emitter**, and pick the `EndScene` signal (in `Signals/`).
5. Press Play: the Timeline plays, and at the End Scene signal the next scene opens.

The Signal Track is bound to the `[Scene Director]` automatically when the scene starts, so you can leave its slot empty.

**Last scene of a chapter** (scenes 4, 8, 12 and 16): tick **Last Scene Of Chapter**. End Scene then opens **Menu Scene** (`01_Menu`) instead of Next Scene.

## How to make the visitor do an action

1. On the Signal Track, right-click where the story should stop and **Add Signal Emitter** with the `Wait` signal.
2. Select the `[Scene Director]`. Under **Wait Steps**, click **+** (one entry per Wait signal, in the same order). Type what the visitor does in **Label**.
3. Under **Show Hint**, click **+**, drag in your trigger (for example the lamp's `RaiseTrigger`), and choose **ShowHint ()**.
4. Under **Force Complete**, do the same and choose **ForceComplete ()**.
5. Select your trigger. Under **On Completed**, click **+**, drag in the `[Scene Director]`, and choose **SceneController → InteractionDone ()**.
6. Press Play: the Timeline stops at Wait and the hint shows. Do the action and it continues. Do nothing and after 20 seconds the action happens by itself and it continues.

The example is `WaitTest/Wait_Test.unity`, with a stand-in `TestAction` (press Space).

## How to add narration to a scene

Every scene's narration and background sound live on its Timeline. Your scene must already have a `[Scene Director]` with a Timeline (see above).

1. **Put your files in place.** Narration goes in `Assets/_Project/Audio/VO/<scene>/`, one file per line, named like `VO_C2_S08_01.wav` (chapter 2, scene 8, line 1).
2. **Set the import settings.** Select all your narration files in the Project window. In the Inspector, tick **Force To Mono**, set **Load Type** to **Compressed In Memory** (never **Streaming**, it can start late on the Quest), and click **Apply**.
3. **Open the Timeline.** Select the `[Scene Director]` and open **Window → Sequencing → Timeline**.
4. **Add the narration track.** Click **+** → **Audio Track**, then rename it exactly `Narration`. Leave its object slot empty: the `[Scene Director]` connects it to the narration sound object when the scene starts (see step 10).
5. **Add the lines in order.** Drag `_01` onto the Narration track at 0 s. Drag `_02` so it starts about **0.5 s** after `_01` ends, then `_03` the same way, and so on. The clips must not overlap.
6. **Add the background sound.** Click **+** → **Audio Track**, rename it exactly `Ambience`, and drag the background sound onto it at 0 s. Drag the clip's right edge until it reaches the end of your scene. Select the clip and tick **Loop** in the Inspector if the sound is shorter than the scene.
7. **Make the background quieter.** Select the `Ambience` track (its name on the left) and set **Volume** to about `0.3` in the Inspector, so the voice is easy to hear.
8. **End the scene after the last line.** Drag the **End Scene** signal on the Signal Track to about **1 s after the last narration clip ends**.
9. **Test.** Press Play. You hear the lines in order over the background, and the next scene opens after the last line.
10. **Connect the sound groups.** Drag Group 4's narration and ambience audio objects into your scene. Select the `[Scene Director]` and drag them into **Narration Source** and **Ambience Source**. The tracks now play through Group 4's `MainMixer`, so the music and ambience get quieter while the voice plays.

The example is `NarrationTest/Narration_Test.unity`: 3 lines (`VO_Test_01` to `_03`, made with Windows text-to-speech) over a generated wind loop (`Amb_Test_Wind`).

## Known problems

- It uses `SceneManager.LoadScene` for now, so there is no fade. It will switch to Group 2's `SceneFlow.GoTo` once that is merged (`// TODO` in `SceneController.cs`).
- The next scene must be in the Build Profiles scene list, or loading it fails. Only the instructor changes that list on `main`; add scenes to your local list only for testing.
- Group 4's `MainMixer` and audio objects aren't merged yet, so step 10 can't be done in real scenes yet. Until then, leave Narration Source and Ambience Source empty: the tracks play straight to the headset, without the music getting quieter.
- `01_Menu` (Group 2) doesn't exist yet. A last scene of a chapter shows an error at End Scene until it's merged and in Build Profiles.
- While the Timeline waits at a Wait signal, the whole Timeline is frozen, including the Ambience track, so it goes quiet. If that's a problem in your scene, ask Group 4 to let their ambience object play the loop by itself instead of putting it on the Timeline.
