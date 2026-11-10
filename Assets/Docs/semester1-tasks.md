# Semester 1: Your Tasks (5 sessions, 8 students)

Read [team-guide.md](team-guide.md) first. It explains how we work together on GitHub and how to use AI.

## The goal for this semester

By the end of session 5 we will have:

1. **The scene template working.** Eight small shared systems, one built by each of you, plugged together.
2. **Your first scene built from the template** (Level 1: greybox, narration, sound, fades).
3. **Scene 8, The Lamp, playable** with its interaction: the visitor raises the lamp.
4. **A narration script and scene card** for each of your two scenes.

Stretch goal: your second scene at Level 1 too. Then all 16 scenes play from start to end.

Not this semester: captions, translations, the delivered 3D characters, real voices and lighting. Those come in the spring.

## Your job in one line

**Each of you builds one shared system and owns two scenes.** Your letter (A to H) is given by the instructor.

| | System you build | Your scenes (first one in bold) | Buddy | Difficulty |
|---|---|---|---|---|
| **A** | Rig, movement and boundary | **1 The mountain**, 2 The empty place | G | ★★☆ |
| **B** | Fader and scene flow (+ Welcome, Credits, headset-off reset) | **4 The grotto**, 3 The hermit uncles | F | ★★☆ |
| **C** | Gaze selection (+ Chapter menu) | **5 Leaving home, a new name**, 6 The scriptorium | D | ★★☆ |
| **D** | Touch and raise interactions (+ tradition label) | **8 The lamp**, 7 Priest, and sixteen years at Annaya | C | ★★☆ |
| **E** | Voice lines (narration on the Timeline) | **10 The cell**, 9 To the hermitage | H | ★★★ |
| **F** | Scene controller | **12 The last Mass**, 11 The locusts | B | ★★★ |
| **G** | Placeholder characters | **13 Burial, and the light**, 14 The exhumation | A | ★☆☆ |
| **H** | Audio (mixer, sounds, voice-over guide) | **16 Annaya today**, 15 Crowds, then sainthood | E | ★☆☆ |

**Your buddy** tests your system with theirs in session 3, and covers for you if you miss a session.

The scene list and what happens in each scene are in [the development plan, section 4](saint-charbel-development-plan.md).

---

## Everyone, every session

| Session | Everyone does |
|---|---|
| **Before session 1** | Install Unity 6000.3.16f1 with Android Build Support, and GitHub Desktop. Accept the GitHub invite, clone the project and open it once (the first open takes 10 to 30 minutes). Steps are in the team guide. |
| **1. Setup** | Run the project on a Quest or the Meta XR Simulator. Make your first pull request (below). Read your card. |
| **2. Build v1** | Build the **minimum version** of your system in your system's test scene. |
| **3. Finish and plug in** | Finish the full version, make it a prefab, write your README, test it with your buddy. Everything merged by the end. |
| **4. First scene** | Build your first scene (bold in the table) at Level 1 from the template. |
| **5. Second scene and showcase** | Build your second scene at Level 1, fix bugs, update your README. We play everything together at the end. |

### Your first pull request (session 1)

1. In GitHub Desktop, make a branch called `yourname/setup`.
2. In Unity, create a folder `Assets/_Project/Sandbox/<YourName>/` and save an empty scene in it called `<YourName>_Sandbox.unity`.
3. Open your system's `README.md` (in `Assets/_Project/Systems/<System>/`) and write 3 lines under "My plan": what you will build first, what you don't understand yet, and which AI tool you'll use.
4. Commit, push, and open a pull request. Done.

### Homework between sessions

These can be done outside class, at home or in extra sessions. Each takes about 1 to 2 hours with AI help.

| Between | Homework |
|---|---|
| Session 1 and 2 | Read your card again. Optional: ask an AI to explain the Unity features your system uses. |
| Session 2 and 3 | **Scene cards and narration scripts** for your 2 scenes (see "Scene homework" below). |
| Session 3 and 4 | **Record placeholder narration** for your 2 scenes (H explains how in session 3). |
| Session 4 and 5 | Finish your first scene if it isn't done. |

---

## The shared systems: how they connect

Each system must offer **exactly** these names, so the pieces plug together without anyone waiting for anyone else. If you think a name should change, ask the instructor; don't change it alone.

All scripts use the namespace `SaintCharbel`.

| System | Owner | What the others use (public API) |
|---|---|---|
| ScreenFader | B | `ScreenFader.Instance.FadeOut(float seconds)` and `FadeIn(float seconds)`. Both return a `Coroutine`, so others can `yield return` them. |
| SceneFlow | B | `SceneFlow.GoTo(string sceneName)`: fade out, load the scene, fade in. |
| AppState | B | `AppState.CurrentChapter` (int, 1 to 4) and `AppState.MovementEnabled` (bool, false by default = Stay seated). |
| Locomotion | A | Part of `[VR Rig]`: teleport and snap turn, on only when `AppState.MovementEnabled` is true. |
| SeatAligner | A | On the rig. `Align()` puts the visitor's head at the object named `Seat`. |
| PlayAreaBoundary | A | On the `Seat` object. `Vector3 size` (default 2 × 2.5 × 2 m). Teleporting only works inside it; leaving it fades the view. |
| HeadsetPresence | B | Added to the rig. No API: after 10 s without the headset, it calls `SceneFlow.GoTo("00_Welcome")`. |
| GazeTarget | C | `float dwellSeconds = 2`, `UnityEvent OnSelected`. |
| GazeTrigger, TouchTrigger, RaiseTrigger | C, D, D | Each has `UnityEvent OnCompleted`, `void ShowHint()` and `void ForceComplete()`. |
| TraditionLabel | D | A prefab. Scenes turn it on with a Timeline Activation track. |
| VoiceLine | E | A ScriptableObject with `AudioClip clip` and `string textEn, textFr, textAr` (the texts stay empty until spring). |
| Voice Line track | E | A Timeline track you drag VoiceLine assets onto. Plays through the Narration mixer group. |
| SceneController | F | On `[Scene Director]`. `string nextScene`, `float timeoutSeconds = 20`, `UnityEvent OnWaitStarted`, `UnityEvent OnTimeout`, `void InteractionDone()`. Signals: `WaitForInteraction` and `EndScene`. |
| Characters | G | Prefabs `Charbel_Placeholder` and `Monk_Placeholder` (Humanoid, with an Animator), plus a `Silhouette` prefab. |
| Audio | H | Mixer `MainMixer` with groups `Narration`, `Music`, `Ambience`, `SFX`. Prefabs `Audio_Narration2D`, `Audio_Music2D`, `Audio_Ambience2D`, `Audio_SFX3D`. |

**How an interaction is wired (no code needed in the scene):**

```text
Timeline reaches the WaitForInteraction signal
  → SceneController pauses the Timeline and fires OnWaitStarted → trigger.ShowHint()
Visitor does the interaction
  → trigger.OnCompleted → SceneController.InteractionDone() → Timeline continues
Visitor does nothing for 20 seconds
  → SceneController.OnTimeout → trigger.ForceComplete() → (same as above)
```

**Need someone's system before it's merged?** Use a stand-in. For example, call `SceneManager.LoadScene` instead of `SceneFlow.GoTo`, and swap it when B's code is merged. Write `// TODO: replace with SceneFlow.GoTo` next to it.

---

## Task cards

Every card has a **minimum version**. If you run short of time, finish the minimum version properly; it's enough for the template to work. The full version is the goal.

### A: Rig, movement and boundary

Development plan chunks 1.1 and 1.2. You already know locomotion; here it's optional and limited to the boundary. Folder: `Assets/_Project/Systems/Rig/`.

| Session | Do | Done when |
|---|---|---|
| 2 | Make the `[VR Rig]` prefab with Meta Building Blocks: Camera Rig, Hand Tracking and Locomotion (**teleport and snap turn only, no smooth movement**). Locomotion turns on only when `AppState.MovementEnabled` is true. Write `SeatAligner`: on start, move and turn the rig so the head is at `Seat`, facing the Seat's forward direction. | You see your hands; whatever way you sit or face, you start at the Seat looking forward; with `MovementEnabled` on you can teleport, and with it off you can't. |
| 3 | Re-align when the headset is put back on. Write `PlayAreaBoundary`: a teleport floor the size of the box, so teleporting only works inside it, and head leaves the box → dark view and a "Please step back" text; back inside → normal. Prefab, README, buddy test with G. | You can't teleport outside the box. Stepping or leaning out of it darkens the view; stepping back restores it. |

- **Minimum version:** rig prefab, Seat alignment on start, and locomotion that can be switched on and off.
- **Uses:** `AppState.MovementEnabled` (B); until it's merged, use a checkbox on your own script. `ScreenFader` (B) for darkening. Until it's merged, use your own black quad in front of the camera.
- **First prompt for the AI:** "Write a Unity C# component SeatAligner for a Meta XR camera rig (OVRCameraRig). On Start, move and rotate the rig root so that the CenterEyeAnchor ends up at the position of a Transform named Seat, facing Seat.forward, rotating only around the Y axis."

### B: Fader and scene flow

Chunks 1.3, 1.4, 1.6 and 1.14, plus the credits. Folder: `Assets/_Project/Systems/SceneFlow/`. You also own the scenes `00_Welcome` and `99_Credits`.

| Session | Do | Done when |
|---|---|---|
| 2 | `ScreenFader` (a black sphere around the camera whose transparency animates; scenes start black and fade in), `SceneFlow.GoTo`, `AppState` (with `CurrentChapter` and `MovementEnabled`). | Two test scenes switch back and forth with a smooth fade, with no flash of the new scene before the fade. |
| 3 | `00_Welcome`: two big choices, **Stay seated** and **Move around** (look at one or touch it). Stay seated is chosen after 10 s. Store it in `AppState.MovementEnabled`, show "Look around. Use your hands when you see this symbol.", then go to `01_Menu`. `99_Credits`: names of the team, sources from the brief, and the sound credits from H. `HeadsetPresence`: headset off for 10 s → `00_Welcome` (Meta SDK event `OVRManager.HMDUnmounted`). README, buddy test with F. | The choice reaches the next scenes; credits fade through, then return to Welcome; headset off for 10 s → Welcome. |

- **Minimum version:** fade in on start, and `GoTo` that fades out and loads.
- **First prompt for the AI:** "In Unity 6 URP for Meta Quest 3, write a ScreenFader singleton that fades a black inside-out sphere around the VR camera. FadeOut(float seconds) and FadeIn(float seconds) return a Coroutine. Then write a static SceneFlow.GoTo(string sceneName) that fades out, loads the scene asynchronously and fades in."

### C: Gaze selection and chapter menu

Chunks 1.5 and 1.7, and `GazeTrigger` from 1.10. Folder: `Assets/_Project/Systems/Gaze/`. You also own the scene `01_Menu`.

| Session | Do | Done when |
|---|---|---|
| 2 | A small dot in the centre of view. `GazeTarget`: looking at it fills a ring over `dwellSeconds`, then `OnSelected` fires. Looking away resets the ring. | You select a test cube by looking at it, and a quick glance past it doesn't select it. |
| 3 | `01_Menu`: four chapter cards (title and years), selectable by gaze or by touching them. The next chapter is highlighted and starts by itself after 10 s. `GazeTrigger` for scenes (wraps `GazeTarget`, with `OnCompleted`, `ShowHint`, `ForceComplete`). Prefab, README, buddy test with D. | The menu starts each chapter, and auto-starts the next one. |

- **Minimum version:** dwell selection with a ring, and a menu that auto-starts chapter 1.
- **Uses:** `SceneFlow.GoTo` (B). Until merged, use `SceneManager.LoadScene`.
- **First prompt for the AI:** "In Unity 6 with the Meta XR SDK, write a GazePointer that raycasts from the center eye camera every frame and a GazeTarget component with a dwellSeconds field and a UnityEvent OnSelected. While looked at, a world-space ring image fills from 0 to 1; looking away resets it."

### D: Touch and raise interactions, tradition label, Scene 8

Chunks 1.10 and 1.13, and the vertical slice 1.16. Folder: `Assets/_Project/Systems/Interactions/`.

| Session | Do | Done when |
|---|---|---|
| 2 | `TouchTrigger` (touch the object with either hand) and `RaiseTrigger` (grab it with Meta's Grab Interaction and lift it about 25 cm above its start). Both fire `OnCompleted`. | Both work in the headset with hand tracking. |
| 3 | Add `ShowHint()` (a hand symbol near the object), `ForceComplete()` (the object does the action by itself: the lamp rises on its own) and a small glow and sound when completed. Make the `TraditionLabel` prefab ("Devotional tradition", small and calm). README, buddy test with C. | Ignoring the object and calling `ForceComplete()` still completes it nicely. |
| 4–5 | **Scene 8, The Lamp, to Level 3**: Level 1 plus Charbel (from G), sounds (from H), the tradition label and the raise interaction. | Scene 8 plays from start to end, with and without the visitor raising the lamp. |

- **Minimum version:** both triggers fire `OnCompleted`, and `ForceComplete()` just fires it too.
- **First prompt for the AI:** "Using the Meta XR Interaction SDK (Grab Interaction building block, Grabbable and HandGrabInteractable, not XR Interaction Toolkit), write a RaiseTrigger component: when the object is grabbed and lifted 0.25 m above its start height, fire UnityEvent OnCompleted once."

### E: Voice lines

Chunk 1.8. Folder: `Assets/_Project/Systems/VoiceLines/`. **This is the hardest system.** Ask for help early.

| Session | Do | Done when |
|---|---|---|
| 2 | The `VoiceLine` ScriptableObject. A custom Timeline track "Voice Line" with a clip that plays the VoiceLine's audio, and whose length matches the audio. | A test Timeline plays 3 voice lines in order. |
| 3 | Route the audio to the `Narration` mixer group (with H). An editor menu item "Create VoiceLines from selected audio": select a folder of WAV files, get one VoiceLine asset per file. README with a GIF or screenshots. Buddy test with H. | Another student turns a folder of WAV files into voice lines on a Timeline in under 2 minutes. |

- **Minimum version:** `VoiceLine` assets, and scenes use Timeline's built-in Audio Track for now. Tell the instructor if you fall back to this.
- **First prompt for the AI:** "In Unity 6 Timeline 1.8, write a custom track: a VoiceLine ScriptableObject (AudioClip clip, string textEn, textFr, textAr), a VoiceLinePlayableAsset clip that plays the clip through an AudioSource and reports its duration, and a VoiceLineTrack. Explain each class in one sentence."

### F: Scene controller

Chunk 1.9. Folder: `Assets/_Project/Systems/SceneController/`.

| Session | Do | Done when |
|---|---|---|
| 2 | `SceneController` on a `[Scene Director]` prefab with a PlayableDirector. It plays the Timeline after the fade in. The `EndScene` signal calls `SceneFlow.GoTo(nextScene)`. | A test scene plays its Timeline, then loads the next scene. |
| 3 | The `WaitForInteraction` signal: pause the Timeline, fire `OnWaitStarted`, and wait for `InteractionDone()` or `timeoutSeconds`. On timeout, fire `OnTimeout` and continue. Ignore `InteractionDone()` when not waiting. At the end of a chapter, go to `01_Menu`. README, buddy test with B. | A test scene pauses, continues after a button press or after 20 s, and moves on. |

- **Minimum version:** play the Timeline, and load the next scene when it ends (no waiting).
- **Uses:** `SceneFlow.GoTo` (B). Until merged, use `SceneManager.LoadScene`.
- **First prompt for the AI:** "In Unity 6 Timeline, write a SceneController with a PlayableDirector. Two Signal assets: WaitForInteraction pauses the director (director.playableGraph.GetRootPlayable(0).SetSpeed(0)) and starts a timeout; EndScene loads nextScene. Public method InteractionDone() resumes. UnityEvents OnWaitStarted and OnTimeout."

### G: Placeholder characters

Chunk 1.11. Folder: `Assets/_Project/Systems/Characters/`.

| Session | Do | Done when |
|---|---|---|
| 2 | Two humanoid placeholder characters (for example from Mixamo): one for Charbel, one for a generic monk. Import as **Humanoid**. Animations: idle, walk, kneel, pray, write, sit, lie down, raise arms. | The placeholder walks to a point and kneels, driven only by a Timeline Animation track. |
| 3 | Prefabs `Charbel_Placeholder`, `Monk_Placeholder` and a `Silhouette` (a dark, simple figure for crowds and villagers). Test swapping the Charbel model for another humanoid. README: "How to animate Charbel on a Timeline" in 6 steps. Buddy test with A (character standing at the right distance from the Seat). | Another student makes Charbel walk and pray on a Timeline using only your README. |

- **Minimum version:** one placeholder with idle, walk and pray.
- **Remember:** Charbel keeps his eyes lowered and moves slowly and calmly (design rule 13).
- **First prompt for the AI:** "Step by step: how do I import a Mixamo character and animations into Unity 6 as Humanoid, and make the character walk to a point and then kneel using only a Timeline Animation track, without root motion problems?"

### H: Audio

Chunks 1.12, 2.6 and 2.7. Folders: `Assets/_Project/Systems/Audio/` and `Assets/_Project/Audio/` (SFX, Ambience, Music).

| Session | Do | Done when |
|---|---|---|
| 2 | `MainMixer` with the 4 groups. Music and Ambience get quieter while narration plays (Duck Volume). The 4 audio-source prefabs (narration, music and ambience 2D; SFX 3D). | In the headset, a sound placed to your left comes from your left, and music dips under narration. |
| 3 | A starter sound library: wind, birds, night insects, a church bell, a chapel interior, a candle and footsteps. Only sounds we're allowed to use (CC0 or recorded by us). Write every file in `Assets/Docs/credits-and-licences.md`. Give a 5-minute talk at the start of session 3: **how to record placeholder narration** (phone or text-to-speech, 48 kHz WAV, file names like `VO_C2_S08_03.wav`). Buddy test with E. | Every student knows how to record and name their narration files. |

- **Minimum version:** the mixer with 4 groups and the prefabs, without ducking.
- **First prompt for the AI:** "In Unity 6, how do I set up an Audio Mixer with Narration, Music, Ambience and SFX groups where Music and Ambience duck automatically while Narration plays (Send and Duck Volume effects)? Step by step."

---

## Scene homework (between sessions 2 and 3)

For **each of your 2 scenes**, create `Assets/Docs/scenes/C<chapter>_S<scene>.md` (for example `C2_S08.md`). Copy the scene card template from [the development plan, section 10](saint-charbel-development-plan.md), fill it in, and add the narration under it.

- About 10 to 14 short lines, for about 90 to 110 seconds of narration. One sentence per line.
- Use the facts and sources in [the project brief](saint-charbel-project-brief.md). Next to each fact, note its source. AI may help you write, but **you** check every fact against the sources: AI can invent dates and names.
- For devotional traditions, use careful wording: "Tradition says…", "Witnesses reported…".
- Visitors are of all faiths and include children. Keep it respectful and simple.

## Building a scene at Level 1 (sessions 4 and 5)

The instructor prepares your scene file from the template. You'll find it in `Assets/_Project/Scenes/Chapter<N>/`. Only open your own scenes.

1. Pull `main`, make a branch `yourname/c2-s08`, and open your scene.
2. Build the environment from simple shapes (cubes, planes, ProBuilder) in `Environments/<scene>/`. Keep it outside the boundary box around the Seat. If your scene is worth exploring, you may make the box bigger; check that it still works from the Seat.
3. Put your narration WAV files in `Audio/VO/<scene>/`, and turn them into voice lines (E's menu item).
4. Open the scene's Timeline: put the voice lines on the Voice Line track, add an ambience clip, and put the `EndScene` signal at the end.
5. Play it in the headset or the Simulator: it fades in, plays the narration, fades out and goes to the next scene.
6. Check the Definition of Done in [the development plan, section 8](saint-charbel-development-plan.md), then commit, push and open a pull request.
