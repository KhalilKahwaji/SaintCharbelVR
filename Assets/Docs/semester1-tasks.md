# Semester 1: What We Build and Who Does What

8 students · 4 groups of 2 · 5 sessions

Also read [team-guide.md](team-guide.md): how we use GitHub, the 5 golden rules, and how to use AI.

---

## 1. What we'll have at the end of the semester

A first version of the app that runs on the Quest from start to end:

1. **Welcome screen:** the visitor chooses **Stay seated** or **Move around**.
2. **Chapter menu:** the 4 chapters of Saint Charbel's life.
3. **Scenes:** at least 2 scenes from every chapter (8 scenes). Each one fades in, plays its narration and sounds, fades out, and goes to the next scene.
4. **Scene 8, The Lamp:** the visitor can grab the lamp and raise it.
5. **Credits** at the end.

All scenes are built from one shared **scene template**, so they all work the same way.

**Bonus:** all 16 scenes.

**Not this semester:** the real 3D models, real voices, captions and translations. They come next semester. Until then we use simple shapes (grey boxes), a placeholder Charbel, and placeholder voices (our own recordings or text-to-speech).

### How one scene works

```text
Fade in  →  The scene's Timeline plays (narration, Charbel, sounds)  →  maybe one small action
(look at something, touch it, or raise it; if the visitor does nothing, it continues by itself after 20 s)
→  Fade out  →  Next scene (or the chapter menu at the end of a chapter)
```

The visitor is **seated by default**. If they chose **Move around**, they can also teleport, but only inside a box around their seat (the **boundary**).

---

## 2. The 4 groups

Each group builds **shared parts** that every scene uses, and owns **one chapter** (4 scenes).

| Group | Builds | Owns chapter |
|---|---|---|
| **1. The Visitor** | The VR rig with hands, the seat, the boundary, Move around (teleport), and the reset when the headset is taken off | **III. The Hermitage** (scenes 9 to 12) |
| **2. Screens and Navigation** | Fading, loading scenes, selecting by looking, the welcome screen, the chapter menu and the credits | **I. Childhood** (scenes 1 to 4) |
| **3. Story Player** | The part that plays each scene's Timeline, the narration setup, and waiting for the visitor's action. Builds the scene template together with the instructor. | **IV. What Followed** (scenes 13 to 16) |
| **4. Objects, Characters and Sound** | Touch and raise actions, the "devotional tradition" label, the placeholder Charbel, and the sound setup | **II. The Monastery** (scenes 5 to 8, including The Lamp) |

Inside each group there's a **Person 1** and a **Person 2**. Decide who is who in session 1. You work side by side and test each other's work.

**🟠 With the instructor** marks the parts that are tricky. Don't do them alone: call the instructor when you reach them.

---

## 3. The 5 sessions

| Session | Everyone |
|---|---|
| **1. Start** | Set up the project, make your first pull request, do your group's **starter task** (section 4). |
| **2. Build** | Build your group's first parts (session 2 in your group's section). |
| **3. Finish and connect** | Finish your parts and test them with the other groups. Everything is merged by the end. Last 40 minutes: write the narration for your chapter (section 6). |
| | *Before session 4, the instructor and Group 3 put the scene template together.* |
| **4. Scenes** | Record placeholder narration, then each person starts a scene of your chapter from the template (section 7). |
| **5. Finish and play** | Finish your scenes and Scene 8, The Lamp. At the end we play everything together on the projector. |

---

## 4. Session 1: start here

### Step 1: Get the project (30 min)

If you haven't yet, follow "One-time setup" in [team-guide.md](team-guide.md): install Unity 6000.3.16f1 with Android Build Support, install GitHub Desktop, clone the project and open it. The first open takes 10 to 30 minutes; start your group's starter task on your partner's computer meanwhile.

### Step 2: Your first pull request (10 min)

Each person:

1. In GitHub Desktop, make a branch called `yourname/setup`.
2. In Unity, create the folder `Assets/_Project/Sandbox/<YourName>/` and save an empty scene in it called `<YourName>_Sandbox.unity`. Your sandbox is yours alone, for trying things out.
3. Commit, push, and create the pull request (team guide, "Every session: the loop").

### Step 3: Your group's starter task (45 to 60 min)

Each starter task is the first piece of your group's real work. Do it in one of your sandbox scenes.

**Group 1: hands in VR**

1. Open **Meta → Tools → Building Blocks** and add **Camera Rig** and **Hand Tracking**.
2. Add a floor (a plane) and a cube 1 m in front of you.
3. Build And Run to a Quest (or press Play with the Meta XR Simulator).

Test:

- [ ] You see your own hands moving in the headset.
- [ ] The cube is in front of you at the right size (about 1 m away).

**Group 2: the welcome panel**

1. Make a world-space UI panel 1.5 m in front of the camera with the title "Welcome" and two buttons: **Stay seated** and **Move around**.
2. Make the buttons pressable with your finger (Building Block: **Poke Interaction**).
3. Each button prints its name in the Console.

Test:

- [ ] Both buttons are easy to read from your seat.
- [ ] Pressing a button with your finger prints its name, once per press.

**Group 3: a first Timeline**

1. Make two scenes in your sandbox, `TestA` and `TestB`. Add both to your *local* Build Profiles list (don't commit that change).
2. In `TestA`, add a Timeline that moves a cube for 5 seconds and plays a sound.
3. At the end of the Timeline, put a **Signal** that loads `TestB`.

Test:

- [ ] Press Play: the cube moves, you hear the sound, and after 5 seconds `TestB` opens.

**Group 4: raise a cube**

1. Add a cube you can grab with your hand (Building Block: **Grab Interaction**).
2. Write a script `RaiseTrigger`: when the cube is lifted 25 cm above where it started, print "Raised!" in the Console, only once.

Test:

- [ ] You can pick up the cube with your hand.
- [ ] Lifting it 10 cm prints nothing. Lifting it 25 cm prints "Raised!" once. Lifting it again prints nothing.

When your starter task works, commit it and open a pull request.

---

## 5. Your group's work

### Group 1: The Visitor

**What you build:** everything about the visitor's body: their view and hands, where they sit, how far they can go, and Move around.

**Your folder:** `Assets/_Project/Systems/Rig/`

| Session | Person 1 | Person 2 |
|---|---|---|
| **2** | Turn the starter rig into a prefab `[VR Rig]`. Then the **seat**: each scene has an empty object called `Seat`. When the scene starts, move the rig so the visitor's eyes are at the Seat, looking the same way the Seat faces. | **Move around:** add teleport and snap turn to the rig (no smooth joystick movement). They work only when `AppState.MovementEnabled` is on (Group 2 makes this; until then use a checkbox on your script). |
| **3** | **The boundary:** a box around the Seat, about 2 × 2 m. When the visitor's head leaves the box, the view goes dark and shows "Please step back". When they come back, it's normal again. | **Teleport only inside the boundary:** put the teleport surface only on the floor inside the box, the same size as the box. Anywhere without a teleport surface, the visitor can't teleport. |
| **3** | **Headset taken off:** after 10 seconds without the headset, go back to the welcome screen. 🟠 With the instructor (the headset events can be tricky) | |

**How to test**

- [ ] Start the scene while sitting turned to the side: you still start at the Seat, facing the scene.
- [ ] With movement off you can't teleport. With movement on you can teleport and snap turn.
- [ ] You can't teleport outside the box.
- [ ] Stand up and step or lean out of the box: the view goes dark and the message shows. Step back: it's normal again.
- [ ] Take the headset off for 10 seconds and put it back on: you're at the welcome screen.

**Other groups use:** the `[VR Rig]` prefab, and a `Seat` object with the boundary on it.

**First question for your AI:** "In Unity 6 with the Meta XR SDK (OVRCameraRig), write a script that, on Start, moves and rotates the rig so the CenterEyeAnchor ends up at a Transform named Seat, facing Seat.forward, rotating only around the Y axis. Explain it simply."

### Group 2: Screens and Navigation

**What you build:** how the visitor moves from screen to screen: fades, scene loading, choosing things by looking at them, and the welcome, menu and credits screens.

**Your folders:** `Assets/_Project/Systems/SceneFlow/` and `Assets/_Project/Systems/Gaze/`. Your scenes: `00_Welcome`, `01_Menu` and `99_Credits`.

| Session | Person 1 | Person 2 |
|---|---|---|
| **2** | **Fade and load:** the view starts black and fades in. `SceneFlow.GoTo("SceneName")` fades to black, loads the scene, and fades in. Also make `AppState`, a small class that remembers `CurrentChapter` (1 to 4) and `MovementEnabled` (on or off) between scenes. | **Select by looking:** a small dot in the middle of the view. Look at an object for 2 seconds and a ring fills up, then the object is selected (`OnSelected` event). Looking away resets the ring. |
| **3** | **Welcome screen:** your starter panel. The choice is saved in `AppState.MovementEnabled`. If nobody chooses in 10 seconds, it picks Stay seated. Then go to the menu. Then the **credits screen**: team names, the sources from the brief, and the sound credits from Group 4; then back to the welcome screen. | **Chapter menu:** 4 chapter cards (title and years), chosen by looking or touching. The next chapter is highlighted and starts by itself after 10 seconds. Also a `GazeTrigger` for scenes: "look at this for 2 seconds" as a scene action (same names as Group 4's actions, see section 8). |

**How to test**

- [ ] Going from one scene to another fades out and in smoothly, with no flash of the new scene.
- [ ] A quick glance at an object doesn't select it. Looking for 2 seconds does.
- [ ] Choose Move around on the welcome screen: in the next scene you can teleport (with Group 1's rig). Do nothing for 10 seconds: Stay seated is chosen.
- [ ] Each chapter card opens the right chapter. Wait 10 seconds: the highlighted chapter starts.
- [ ] Credits show and return to the welcome screen.

**Other groups use:** `SceneFlow.GoTo`, `AppState`, and `GazeTrigger`.

**First question for your AI:** "In Unity 6 URP for Meta Quest 3, write a ScreenFader that fades a black sphere around the VR camera in and out, and a static SceneFlow.GoTo(string sceneName) that fades out, loads the scene, and fades in. Explain it simply."

### Group 3: Story Player

**What you build:** the part that plays each scene. Every scene has a Timeline with everything in it (narration, Charbel's movements, sounds), and your `SceneController` runs it from start to end.

**Your folder:** `Assets/_Project/Systems/SceneController/`

| Session | Person 1 | Person 2 |
|---|---|---|
| **2** | **Scene Director:** a prefab `[Scene Director]` with a Timeline and your `SceneController`. When the scene starts it plays the Timeline. At the **End Scene** signal it goes to the scene named in `nextScene` (use `SceneManager.LoadScene` until Group 2's `SceneFlow.GoTo` is merged). | **Narration on the Timeline:** a test scene with 3 short narration clips (record yourself or use text-to-speech) on an Audio Track, plus background sound on another track. Write the steps "How to add narration to a scene" in your README; every group will follow them. |
| **3** | **Waiting for the visitor:** at a **Wait** signal, the Timeline pauses and a hint shows. It continues when the visitor does the action, or by itself after 20 seconds. 🟠 With the instructor (pausing a Timeline correctly is tricky) | **End of chapter:** the last scene of a chapter goes to the menu. Then connect the narration track to Group 4's Narration sound group, so the music gets quieter under the voice. |
| **before 4** | **The scene template**, with the instructor: one scene that has everyone's parts in it, which all scenes are copied from. | |

**How to test**

- [ ] Your test scene fades in, plays the 3 narration clips in order, fades out, and opens the next scene.
- [ ] At the Wait signal the Timeline stops. Press a test button: it continues. Don't press: it continues after 20 seconds.
- [ ] The last scene of a chapter opens the menu.
- [ ] The music gets quieter while the narration plays.

**Other groups use:** the `[Scene Director]` prefab, its `nextScene` field, the **Wait** and **End Scene** signals, and `InteractionDone()` (section 8).

**First question for your AI:** "In Unity 6 Timeline, write a SceneController with a PlayableDirector. A Signal called EndScene loads the scene named in a nextScene field. Explain how Signal tracks and Signal Receivers work, simply."

### Group 4: Objects, Characters and Sound

**What you build:** the things the visitor can do with objects, the placeholder Charbel, and how everything sounds.

**Your folders:** `Assets/_Project/Systems/Interactions/`, `Assets/_Project/Systems/Characters/` and `Assets/_Project/Systems/Audio/`. Sound files go in `Assets/_Project/Audio/`.

| Session | Person 1 | Person 2 |
|---|---|---|
| **2** | **Touch and raise:** `TouchTrigger` (touch the object with either hand) and your starter `RaiseTrigger`. Both have an `OnCompleted` event, `ShowHint()` (shows a small hand symbol near the object) and `ForceComplete()` (the action happens by itself, for example the lamp rises on its own). | **Sound setup:** an Audio Mixer called `MainMixer` with 4 groups: Narration, Music, Ambience, SFX. Music and Ambience get quieter while Narration plays. Make 4 ready-made audio objects: narration, music and ambience (heard the same everywhere) and SFX (heard from where the object is). |
| **3** | **Finishing touches:** a soft glow and a small sound when an action is completed. The **tradition label**: a small, calm "Devotional tradition" sign that scenes can turn on. A starter set of sounds (wind, birds, bells, candle, footsteps) that we're allowed to use (free CC0 sounds or our own recordings), each written in `Assets/Docs/credits-and-licences.md`. | **Placeholder characters:** a Charbel and a monk (for example from Mixamo), set to **Humanoid**, with animations: idle, walk, kneel, pray. Make Charbel walk to a point and kneel, using only a Timeline. Charbel moves slowly and calmly, with his eyes lowered. 🟠 With the instructor if the character slides, floats or snaps back. |

**How to test**

- [ ] Touching the object with either hand completes it, only once.
- [ ] Lifting 25 cm completes the raise; 10 cm doesn't.
- [ ] Calling `ForceComplete()` makes the object do its action by itself.
- [ ] A sound placed to your left comes from your left in the headset.
- [ ] Music gets quieter when narration plays.
- [ ] Charbel walks and kneels on the Timeline without sliding or floating.

**Other groups use:** `TouchTrigger`, `RaiseTrigger`, the tradition label, the 4 audio objects, and `Charbel_Placeholder`.

**First question for your AI:** "In Unity 6, how do I set up an Audio Mixer with Narration, Music, Ambience and SFX groups where Music and Ambience automatically get quieter while Narration plays? Step by step."

---

## 6. Writing your chapter's narration (session 3, last 40 minutes)

Each group writes the narration for the 4 scenes of its chapter. Split them: 2 scenes per person.

1. Read your chapter in [the project brief](saint-charbel-project-brief.md) ("Key moments of his life") and your scenes in [the development plan, section 4](saint-charbel-development-plan.md).
2. For each scene, create a file `Assets/Docs/scenes/C<chapter>_S<scene>.md`, for example `C2_S08.md` for Chapter II, scene 8.
3. Write 10 to 14 short lines, one sentence per line. That's about 1.5 minutes of speaking.
4. Next to each fact, write where it comes from (a source in the brief). AI may help you write, but **you check every fact**: AI can invent dates and names.
5. For traditions, use careful words: "Tradition says…", "Witnesses reported…".
6. Visitors are of all faiths and include children. Keep it respectful and simple.

Ask the instructor to read it before you record it.

---

## 7. Building your scenes (sessions 4 and 5)

Each person builds 2 scenes of the group's chapter. Start with the scenes marked **must**.

| Group | Chapter | Must (one per person) | If there's time |
|---|---|---|---|
| 1 | III. The Hermitage | 10 The cell, 12 The last Mass | 9 To the hermitage, 11 The locusts |
| 2 | I. Childhood | 1 The mountain, 4 The grotto | 2 The empty place, 3 The hermit uncles |
| 3 | IV. What Followed | 13 Burial, and the light, 16 Annaya today | 14 The exhumation, 15 Crowds, then sainthood |
| 4 | II. The Monastery | 8 The lamp (with the raise action), 5 Leaving home, a new name | 6 The scriptorium, 7 Priest, and sixteen years at Annaya |

The instructor prepares every scene file from the template. You'll find yours in `Assets/_Project/Scenes/Chapter<N>/`. Only open your own scenes.

**Session 4, first 20 minutes: record placeholder narration.** Record your scene's lines with your phone or a text-to-speech tool. One file per line, named like `VO_C2_S08_03.wav` (chapter 2, scene 8, line 3). Put the files in `Assets/_Project/Audio/VO/<scene>/`.

**Then build the scene:**

1. Pull `main`, make a branch `yourname/c2-s08`, and open your scene.
2. Build the place from simple shapes (cubes, planes, ProBuilder) in `Assets/_Project/Environments/<scene>/`. Keep everything outside the boundary box around the Seat.
3. Open the scene's Timeline. Put your narration files on the narration track in order, add background sound, and move the **End Scene** signal to the end. Follow Group 3's README.
4. Play it in the headset.

**Your scene is done when:**

- [ ] It fades in, plays the narration in order, fades out, and goes to the next scene.
- [ ] You can hear the narration clearly over the background sound.
- [ ] It works when seated, and also with Move around (you can't teleport outside the box).
- [ ] Nothing is inside the boundary box.
- [ ] There are no red errors in the Console.
- [ ] Someone from another group played it and found no problems.

---

## 8. Names everyone must use

Groups connect their work through these names. Don't change them without asking the instructor. If you need a part another group hasn't merged yet, use a simple stand-in and write `// TODO` next to it.

All scripts use the namespace `SaintCharbel`.

| Name | Made by | What it does |
|---|---|---|
| `[VR Rig]` | Group 1 | The visitor: camera, hands, teleport |
| `Seat` | Group 1 | Where the visitor starts, with the boundary box on it |
| `SceneFlow.GoTo("SceneName")` | Group 2 | Fade out, load the scene, fade in |
| `AppState.MovementEnabled` | Group 2 | On if the visitor chose Move around |
| `AppState.CurrentChapter` | Group 2 | The chapter being played (1 to 4) |
| `[Scene Director]` | Group 3 | Plays the scene's Timeline; has a `nextScene` field |
| **Wait** and **End Scene** signals | Group 3 | Put them on a Timeline to pause for an action or end the scene |
| `InteractionDone()` | Group 3 | Call it when the visitor's action is done; the Timeline continues |
| `GazeTrigger`, `TouchTrigger`, `RaiseTrigger` | Groups 2 and 4 | Scene actions. Each has `OnCompleted`, `ShowHint()` and `ForceComplete()` |
| `MainMixer` and the 4 audio objects | Group 4 | Narration, Music, Ambience, SFX |
| `Charbel_Placeholder` | Group 4 | The placeholder Charbel |

**How a scene action is connected (in the Inspector, no code):**

```text
Timeline reaches Wait   →  Scene Director pauses and calls ShowHint() on the action
Visitor raises the lamp →  RaiseTrigger.OnCompleted  →  Scene Director's InteractionDone()  →  the Timeline continues
Nothing for 20 seconds  →  Scene Director calls ForceComplete()  →  the lamp rises by itself  →  the Timeline continues
```

---

## 9. When you're stuck

Stuck for 20 minutes? Ask, in this order:

1. **Your AI tool.** Paste the project context from [team-guide.md](team-guide.md) and the exact error.
2. **Your partner.**
3. **The group whose part you're using.**
4. **The instructor.**

Anything marked 🟠: call the instructor right away.
