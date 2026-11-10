# Saint Charbel VR: Development Plan (Stage 1, "The Journey")

This plan turns the [project brief](saint-charbel-project-brief.md) into small tasks ("chunks"). Finish every chunk and you have a finished, installable product, **ready by July 2027**.

It is written for students who are learning Unity and VR. If a word is new to you, look for the short explanation next to it.

**How to use it**

1. Read sections 1 to 5 once. They explain what we are building and the rules that keep it simple.
2. Pick a chunk from section 6 that is not taken, put your name on the team board, and do it.
3. A chunk is finished only when everything under **Done when** is true.
4. Never start a chunk whose **Needs** are not finished yet.

**Decisions so far (November 2026)**

- **Narration:** a narrator voice-over, with captions in English, French and Arabic. Captions are added *after* the scenes work without them (Phase 6).
- **Characters:** about 2 delivered characters, arriving sometime in the Spring 2027 semester. Until then, and for every other figure, we use placeholders and silhouettes.
- **Deadline:** the finished product is ready by **1 July 2027**.

---

## 1. The finished product

A visitor sits on a chair (they may stand or move a little inside a marked area), puts on a Meta Quest 3 and:

1. Chooses a caption language (English, Français, العربية) by looking at it.
2. Sees a short welcome that asks them to stay seated.
3. Watches four chapters of Saint Charbel's life, 16 scenes in all, about 26 minutes. A narrator tells the story, with captions in their language.
4. In a few scenes they can do one simple thing with their hands or eyes, such as raising the lamp.
5. Between chapters they see a chapter menu. The next chapter starts on its own after a few seconds.
6. At the end they see credits. When they take the headset off, the app resets for the next visitor.

**The product is finished when** all 16 scenes play from start to end on a Quest 3 with captions in all three languages, at a smooth frame rate, with the delivered characters and voices, without anyone needing to touch a computer, and the library staff have a one-page guide for running it.

---

## 2. Design rules (these keep the project simple)

Every scene must follow these rules. If you think a scene needs to break one, ask the instructor first.

1. **The visitor is seated by default, and can move only inside a boundary.** Scenes are designed for someone sitting. A visitor may still lean, stand or take a step, but each scene has a boundary (play area) around the seat. If they leave it, the view fades and asks them to come back. There is no joystick or teleport movement.
2. **The visitor does not control a character.** Charbel is like an actor in a film. His movements are played by Timeline (Unity's tool for sequencing animation, sound and events), not by the visitor.
3. **One scene = one Timeline.** Everything that happens in a scene (Charbel's animation, narration, sounds, lights turning on, and later the captions) is placed on that scene's Timeline.
4. **At most one interaction per scene.** Only three kinds exist: *look at something*, *touch something*, *grab and raise something*.
5. **Interactions are always optional.** If the visitor does nothing for 20 seconds, the scene continues by itself. Nobody can get stuck.
6. **Prefer shared pieces over new code.** Build scenes from the shared template pieces (prefabs and components) whenever they can do the job. If a scene really needs its own script, keep it small, put it in that scene's script folder (section 8), and have the tech lead review it. If two scenes need the same thing, turn it into a shared piece.
7. **Every scene starts and ends with a fade from/to black.** Scene changes never cut abruptly.
8. **The camera never moves on its own.** The only exception is a very slow forward drift during a fade. Unwanted camera movement makes people feel sick.
9. **Keep the important action in front of the visitor**, within about 45° left or right. Use a sound to draw their attention if something happens to the side.
10. **Placeholders first.** Build with simple shapes and a placeholder character. Swap in the delivered 3D later.
11. **Plan for few characters.** Expect about two delivered characters: Charbel and a generic monk (section 9). Design every scene so it also works if other figures (family, villagers, crowds, pilgrims) are silhouettes, distant figures or only sound. If more characters are delivered, use them.
12. **Devotional traditions are labelled on screen.** This applies to the lamp, the locusts and the light over the grave, and to any other tradition the reviewers flag.
13. **Charbel's humility is part of his character.** He keeps his eyes lowered in front of others, never speaks loudly and makes no big gestures.

---

## 3. How the app is organised

### Scene flow

```text
00_Welcome  (caption language choice, added in Phase 6; "please stay seated")
     │
01_Menu  (chapter menu, next chapter highlighted, auto-starts after 10 s)
     │
Chapter I scenes 1-4 → back to Menu → Chapter II scenes 5-8 → Menu →
Chapter III scenes 9-12 → Menu → Chapter IV scenes 13-16
     │
99_Credits  (credits, sources, thank you)
     │
Headset taken off for 10 s → back to 00_Welcome for the next visitor
```

### What is inside every story scene

```text
C2_S08_TheLamp.unity
 ├─ [VR Rig]          prefab: camera, hands, fader, gaze pointer, seat aligner,
 │                     headset-off reset (and later the caption display).
 │                     Never edit it inside a scene.
 ├─ [Scene Director]  prefab: the Timeline (PlayableDirector) + SceneController
 ├─ Environment       greybox shapes now, delivered 3D later
 ├─ Charbel           placeholder character now, delivered character later
 ├─ Seat + Boundary   where the visitor's eyes start, and the area they may move in
 ├─ Interaction       optional, at most one (gaze, touch or grab-and-raise)
 └─ Audio             ambience and sound-effect sources placed in the world
```

### What is on every scene's Timeline

| Track | What it does |
|---|---|
| Charbel: Animation | Walk, pray, kneel, write and so on (animation clips) |
| Charbel: Position | Where he stands and walks (recorded movement) |
| Voice lines | Narration audio; later also shows the caption for each line (shared custom track) |
| Music / Ambience / SFX | Audio clips |
| Activation | Turns objects on and off (tradition label, props, lights) |
| Signals | "Wait for interaction" and "End scene" markers |

### Shared pieces (built once, used by every scene)

| Piece | Type | Purpose | Built in |
|---|---|---|---|
| VR Rig | Prefab | Camera rig, hand tracking, and all the systems below | 1.1 |
| SeatAligner | Script | Places the visitor's head at the scene's viewing seat, facing forward | 1.2 |
| PlayAreaBoundary | Script | The area around the seat; leaving it fades the view and asks the visitor to come back | 1.2 |
| ScreenFader | Script | Fades to and from black | 1.3 |
| SceneFlow | Script | Loads the next scene with a fade; remembers the chapter (and later the language) | 1.4 |
| GazePointer + GazeTarget | Scripts | Select things by looking at them (dot plus filling circle) | 1.5 |
| VoiceLine | ScriptableObject | One narration line: its audio, plus caption text in 3 languages (filled in Phase 6) | 1.8 |
| Voice line track | Timeline track | Plays a VoiceLine; in Phase 6 it also shows its caption for exactly that long | 1.8 |
| SceneController | Script | Plays the Timeline, pauses at wait points, handles timeouts, goes to the next scene | 1.9 |
| GazeTrigger, TouchTrigger, RaiseTrigger | Scripts | The only three interaction types | 1.10 |
| Charbel_Placeholder | Prefab | Humanoid placeholder that the delivered character replaces | 1.11 |
| Audio Mixer | Asset | Narration, Music, Ambience, SFX groups; music gets quieter under narration | 1.12 |
| TraditionLabel | Prefab | "Devotional tradition" label (English first, 3 languages in Phase 6) | 1.13 |
| HeadsetPresence | Script | Headset removed for 10 s → reset to the welcome screen | 1.14 |
| LocalizedText | Script | A text that shows English, French or Arabic depending on the chosen language | 6.1 |
| CaptionDisplay | Script | Shows captions in front of the visitor, readable and calm | 6.3 |

The VoiceLine asset has the caption text fields **from the start**, even though they stay empty until Phase 6. That way captions plug in later without rebuilding any Timeline.

---

## 4. The 16 scenes (proposal)

This list fits the key moments in the brief into 16 scenes, four per chapter, of about 90 to 110 seconds each (26 minutes total). It is a **proposal**: chunk 2.1 checks it against the President deck and gets Dr. Abbas's approval before scenes are built.

**Priority:** *Core* scenes must ship. *Extra* scenes ship if there is time. If not, they stay simple tableaux (section 5) or their story is folded into the narration of a neighbouring scene.

**Who appears:** *Charbel* and *Monk* are the two delivered characters (section 9). Anything else is a silhouette or sound.

| # | Scene | Years | What the visitor sees (placeholder-friendly) | Figures | Interaction | Label | Priority |
|---|---|---|---|---|---|---|---|
| **I. Childhood** | | | | | | | |
| 1 | The mountain | 1828 | Dawn over Bqaa Kafra (1,600 m) and the Qadisha Valley; village houses; a church bell. Opening shot. | none | none | | Core |
| 2 | The empty place | 1831 | The family home; his father's place left empty (he was taken for forced labour and died). Quiet. | silhouettes | none | | Extra |
| 3 | The hermit uncles | c. 1840s | A mountain path; two hermits praying in the distance. | Monk ×2, distant | none | | Extra |
| 4 | The grotto | c. 1840s | A cave above the village, lit by a candle; a small silhouette of the boy praying. Chapter ending. | silhouette | Look at the grotto → move inside (with a fade) | | Core |
| **II. The Monastery** | | | | | | | |
| 5 | Leaving home, a new name | 1851–1853 | Night; a young man leaves the village without telling his family → monastery gate at Mayfouq → Annaya; the name "Charbel" appears. | Charbel (from behind) | none | | Core |
| 6 | The scriptorium | 1850s | A desk with manuscripts and herbs; his teacher, Saint Nimatullah Al-Hardini. | Charbel, Monk (Hardini) | Touch the manuscript → ink appears | | Extra |
| 7 | Priest, and sixteen years at Annaya | 1859–1875 | Ordination at Bkerke (simple altar), then monks working the fields. | Charbel, Monk ×several | none | | Core |
| 8 | The lamp | 1859–1875 | Night; a lamp filled with water still burns. | Charbel | Grab the lamp and raise it (from the deck) | Yes | Core |
| **III. The Hermitage** | | | | | | | |
| 9 | To the hermitage | 1875 | The path up to the Hermitage of Saints Peter and Paul. | Charbel | none | | Core |
| 10 | The cell | 1875–1898 | Small cell: altar with an image of the Virgin, another image by the bed; Charbel praying, eyes lowered. | Charbel | Look at the image of the Virgin → a candle lights | | Core |
| 11 | The locusts | 1885 | A swarm darkens the sky over Annaya (particle effect). Not in the deck; confirm before building. | none | none | Yes | Extra |
| 12 | The last Mass | 1898 | The hermitage chapel; the stroke during Mass shown gently (he steadies himself at the altar, fade). Death on 24 December. | Charbel | none | | Core |
| **IV. What Followed** | | | | | | | |
| 13 | Burial, and the light | 1898–1899 | A simple grave in the ground, no coffin; a soft light over it for many nights. | none | none | Yes | Core |
| 14 | The exhumation | 1899 | The new tomb inside the monastery. Show the coffin and tomb, never the body. | none | none | Check with reviewers | Extra |
| 15 | Crowds, then sainthood | 1950–1977 | Crowds suggested by sound and silhouettes; dates of beatification (1965) and canonization (1977). | silhouettes | none | | Extra |
| 16 | Annaya today | Today | Pilgrims of all faiths as silhouettes; candles; Pope Leo XIV's visit (1 Dec 2025) mentioned. Closing. | silhouettes | Touch a candle to light it | | Core |

Core scenes: 1, 4, 5, 7, 8, 9, 10, 12, 13, 16 (10 scenes). Extra scenes: 2, 3, 6, 11, 14, 15 (6 scenes).

**Chapter I needs no delivered character.** It is told through places, objects, light and narration. The Charbel character is needed from scene 5 onward.

---

## 5. Build levels: how we always have a finished product

Every scene is built in levels. **All 16 scenes reach Level 1 before any scene goes to Level 2.** That way there is a complete experience you can play from start to end early, and it only gets better from there.

| Level | Name | What it contains | Possible after |
|---|---|---|---|
| 1 | **Tableau** | Greybox environment, placeholder narration (voice-over), ambience sound, fades, correct duration | Phase 1 |
| 2 | **Alive** | Plus placeholder Charbel or Monk animated on the Timeline, sound effects, music, simple lighting | Phase 1 |
| 3 | **Interactive** | Plus the scene's one interaction (only scenes 4, 6, 8, 10, 16) and polish | Phase 1 |
| 4 | **Delivered** | Delivered characters, 3D and voices swapped in, lighting baked | Phase 5 (Spring 2027) |
| 5 | **Captioned** | Captions in English, French and Arabic, and a language choice at the start | Phase 6 |

Captions come last on purpose. They follow the narration audio automatically, so adding them after the real voice-over means they are timed only once.

---

## 6. The chunks

Sizes: **S** = about one lab session, **M** = 2 to 3 sessions, **L** = 4 to 6 sessions. These are estimates for learners; that's fine.

### Phase 0: Setup

Most of this was done while setting up the project: Meta XR All-in-One SDK 207, OpenXR, Meta's Project Setup Tool fixes, the OVRCameraRig scene.

**0.1 Finish the Android (Quest) setup** · S · tech lead / instructor
- Install Android Build Support for Unity 6000.3.16f1. It failed earlier because C: was full.
- Switch the build target to Android, set texture compression to ASTC, and run Meta's Project Setup Tool and Unity's Project Validation again until no required items remain.
- **Done when:** Project Validation shows no errors for Android or Windows.

**0.2 Clean up the project** · S · tech lead
- Remove the URP template leftovers (`Readme.asset`, `TutorialInfo`).
- Create the folder structure from section 8.
- **Done when:** the project opens with no errors and the folders exist.

**0.3 Everyone runs VR** · S each · every student · Needs 0.1
- Put a headset in developer mode, build and run the sample scene on it, and play it in the Editor using Quest Link or the Meta XR Simulator (works without a headset).
- **Done when:** every student has seen their own build running on a Quest.
- **Learn:** the build pipeline, developer mode, testing without a headset.

**0.4 Team workflow** · S · whole team
- Set up Git with LFS (already configured in `.gitattributes`) and GitHub Desktop, assign tasks, and read the Git rules (section 8) and [team-guide.md](team-guide.md). Semester 1's tasks are in [semester1-tasks.md](semester1-tasks.md).
- **Done when:** every student has cloned, changed something, committed and pushed.

### Phase 1: The template (shared systems, cohort one)

Build these in order; each one is small. Test every chunk in the headset before marking it done.

**1.1 VR Rig prefab** · S · Needs 0.3
- Use Meta's Building Blocks (Meta menu → Tools → Building Blocks): Camera Rig and Hand Tracking. Add no joystick or teleport movement. Save as `Systems/Rig/Prefabs/[VR Rig]`.
- **Done when:** you see your tracked hands in the headset, and you can only move by physically moving your body.
- **Learn:** prefabs, the camera rig, hand tracking.

**1.2 Seat aligner and boundary** · M · Needs 1.1
- An empty object named "Seat" marks where the visitor's eyes should be. On start, and when the headset is put on, the rig is moved and turned so the visitor's head is at the Seat, facing its forward direction. This works the same for tall and short visitors and does not depend on the headset's own recenter.
- A "Boundary" box around the Seat (start with about 2 × 2 m, and no bigger than the real free space around the chair) marks where the visitor may move. If their head leaves it, the view fades to dark with a short "Please step back" message, and fades back in when they return. Walls and objects in the scene should sit outside this area, so nobody reaches into a virtual wall.
- **Done when:** however you sit or face when the scene starts, you end up at the Seat looking forward; and stepping out of the boundary fades the view, while stepping back in restores it.

**1.3 Screen fader** · S · Needs 1.1
- A black sphere or quad in front of the eyes whose transparency animates. Provides `FadeOut(seconds)` and `FadeIn(seconds)`. Scenes start black and fade in.
- **Done when:** the scene fades in on start and you can fade out with a test button.

**1.4 Scene flow** · M · Needs 1.3
- `SceneFlow.GoTo(sceneName)`: fade out, load the scene, fade in. A small static `AppState` stores the current chapter. The language is added in 6.2. Scenes are listed in Build Profiles in story order.
- **Done when:** two test scenes switch back and forth with fades.
- **Learn:** scene loading, static data.

**1.5 Gaze selection** · M · Needs 1.1
- A small dot in the centre of view. Looking at an object with a `GazeTarget` shows a circle filling up over 2 seconds, then the object's `OnSelected` event fires. Looking away cancels.
- **Done when:** you can select a test cube by looking at it, and glancing past it does not select it.
- **Learn:** raycasts, UnityEvents.

**1.6 Welcome scene** · S · Needs 1.4
- `00_Welcome`: a short message ("Please stay seated. Look around. Use your hands when you see this symbol."), then go to the menu. The language choice is added in 6.2.
- **Done when:** the welcome plays and leads to the menu.

**1.7 Chapter menu and credits** · M · Needs 1.5, 1.6
- `01_Menu`: four chapter cards (title and years) selectable by gaze, or by touching them (Building Block: Poke Interaction). The next chapter is highlighted and starts by itself after 10 seconds. Titles are in one language for now; they are translated in 6.2.
- `99_Credits`: students and cohorts, faculty, sources, music and sound licences.
- **Done when:** the menu can start each chapter, and it auto-starts the next one.

**1.8 Voice lines (narration)** · L · Needs 1.4
- `VoiceLine` asset: one narration audio clip, plus empty English, French and Arabic text fields for later captions.
- A custom Timeline "Voice line" track: drag a VoiceLine onto it, and it plays that line's audio. In Phase 6 the same track will also show the caption for exactly the clip's length.
- **Done when:** a test Timeline plays 3 voice lines in order.
- **Learn:** ScriptableObjects, custom Timeline tracks (tech lead writes the track; others use it).

**1.9 Scene controller** · M · Needs 1.4, 1.8
- `SceneController` plays the scene's Timeline. A **Wait for interaction** signal pauses the Timeline until the interaction is done or 20 seconds pass. During the wait, a soft hint shows, and after the timeout the interaction plays by itself. An **End scene** signal goes to the next scene, or to the menu at the end of a chapter.
- **Done when:** a test scene pauses, continues after the interaction or the timeout, and moves on to the next scene.
- **Learn:** Timeline signals, PlayableDirector.

**1.10 Interaction kit** · L · Needs 1.9
- **GazeTrigger:** look at the object for 2 seconds (reuses 1.5).
- **TouchTrigger:** touch the object with either hand (a trigger collider on the hands).
- **RaiseTrigger:** grab the object (Building Block: Grab Interaction) and lift it about 25 cm above where it started.
- All three tell the SceneController "done", show a hand or eye hint symbol, and play a small sound and glow when completed.
- **Done when:** each works in the headset with hand tracking, and each scene still continues after its timeout if ignored.

**1.11 Placeholder characters** · M · Needs 1.9
- Two humanoid placeholders with standard humanoid rigs (for example Mixamo characters), one for Charbel and one for the generic Monk, plus placeholder animations: idle, walk, kneel, pray, write, raise, sit, lie.
- Set both as **Humanoid** in Unity so the delivered characters can reuse the same animations and Timelines (retargeting). Save as `Systems/Characters/Prefabs/Charbel_Placeholder` and `Monk_Placeholder`.
- **Done when:** the placeholder walks to a point and kneels, driven only by a Timeline, and swapping in a different humanoid model keeps working.
- **Learn:** Humanoid rigs, Animation tracks, swapping models.

**1.12 Audio setup** · M · Needs 1.8
- Audio Mixer groups: Narration, Music, Ambience, SFX. Music and ambience get quieter under narration (Duck Volume effect). Narration and music are 2D (not positioned). Sounds made by things in the scene are 3D (Spatial Blend = 1) and placed on the object. Create prefabs for each kind of audio source.
- **Done when:** in the headset, the narration is always clear, and a sound placed to your left comes from your left.

**1.13 Devotional tradition label** · S · Needs 1.4
- A small, calm "Devotional tradition" label that a scene turns on with an Activation track. English for now; French and Arabic are added in 6.2.
- **Done when:** the label is readable but not distracting in the headset.

**1.14 Headset off → reset** · S · Needs 1.4
- When the headset is taken off (Meta SDK event `OVRManager.HMDUnmounted`) for 10 seconds, go back to `00_Welcome`. When it is put back on, run the Seat aligner. Check that these events fire with the OpenXR setup.
- **Done when:** taking the headset off mid-scene and putting it back on shows the welcome screen.

**1.15 Scene template and guide** · M · Needs 1.1–1.14
- `Scenes/_Template/Scene_Template.unity` with everything in section 3 already wired. Write `Docs/how-to-build-a-scene.md`: duplicate the template, rename it, add the environment, build the Timeline, add voice lines, and test, in about 10 steps with screenshots.
- **Done when:** a student who did not build the template makes a new Level 1 scene using only the guide.

**1.16 Vertical slice: Scene 8, The Lamp** · L · Needs 1.15 → **Milestone M1**
- Build one scene fully to Level 3 using only the template. It uses nearly every system (character, narration, label, interaction, audio), so it shows the template is complete.
- **Done when:** it meets the scene "Definition of Done" (section 8) and has been played by someone outside the team.

### Phase 2: Story and content (runs in parallel with Phase 1, little coding)

These chunks suit students who prefer writing, research, sound or organisation. Many can start now.

**2.1 Lock the scene list** · S · producer
- Compare section 4 with the President deck's beats. Decide the hermit uncles, locusts and exhumation scenes. Get Dr. Abbas's approval.
- **Done when:** a final list of 16 scenes, with priorities, is written in this file and approved.

**2.2 Scene cards** · M · scene owners · Needs 2.1
- Fill in a one-page card (section 10) for each scene: what the visitor sees, who appears, the interaction, sounds, duration, and assets needed.
- **Done when:** 16 cards exist and the instructor has read them.

**2.3 Narration script** · L · writers · Needs 2.1
- Write the narration line by line for each scene in the voice-over language (open question Q2): short sentences, about 10 to 14 lines per scene. Check every fact against the sources in the brief.
- Use careful wording for traditions ("Tradition says…", "Witnesses reported…"). Keep the audience in mind: visitors of all faiths, including children.
- **Done when:** the script is fact-checked, with a source noted for each fact, and approved by Dr. Abbas.

**2.4 Content review** · M · producer · Needs 2.3
- Review with Dr. Abbas and a church advisor (for example from the Lebanese Maronite Order, which cares for Annaya): accuracy, tradition labels, how his death, burial and exhumation are shown, and Charbel's appearance.
- **Done when:** written approval or a list of changes.

**2.5 Asset request package for the 3D faculty member** · M · asset liaison · Needs 2.2 · **send before the end of Fall 2026**
- Send one document containing:
  - The technical spec (section 9).
  - The two characters we recommend (section 9).
  - The animation list per scene, environments, props and the final voice-over script, each with a priority.
- Ask for the characters and voices **by the end of March 2027**, so there is time to integrate and test before July.
- **Done when:** sent, acknowledged, with delivery dates agreed and written on the board.

**2.6 Placeholder voice-over** · M · audio · Needs 2.3
- Record students reading the script, or use text-to-speech. Cut it into one file per line, named as in section 8, and create the VoiceLine assets.
- **Done when:** every scene has a placeholder voice line for every script line.

**2.7 Sounds, music and licences** · M · audio
- Make a sound list per scene (ambience, SFX, music). Use only sounds you are allowed to use in a permanent public installation (for example CC0 sounds or self-recorded). Record the source and licence of every file in `Docs/credits-and-licences.md`.
- **Done when:** every scene has its sounds and every file appears in the credits file.

**2.8 Caption translations** · L · translators · Needs 2.4 · needed by Phase 6
- Translate the approved script into the other caption languages and have each one reviewed. Keep the name glossary (section 9) so spellings stay the same everywhere.
- **Done when:** every line exists in English, French and Arabic, reviewed.

### Phase 3: Every scene at Level 1 (Tableau) → Milestone M3

**3.x Build scene N at Level 1** · M each · one owner per scene · Needs 1.15, 2.6
- Duplicate the template → greybox environment (simple shapes, ProBuilder if you like) → place the Seat → voice lines on the Timeline → ambience → fades → correct duration.
- **Done when:** the scene plays start to end and goes to the next scene.

**3.17 Full playthrough** · S · QA lead
- Play Welcome → all 16 scenes → Credits in one go in the headset.
- **Done when:** no stops, crashes or wrong order. **This is the first complete product.**

### Phase 4: Make scenes Alive (Level 2) and Interactive (Level 3)

**4.x Bring scene N to Level 2** · M each · Needs 1.11, 1.12
- Add the placeholder Charbel or Monk on the Timeline (see the Figures column in section 4), plus SFX, music and simple lighting. Follow rule 13 (humility).

**4.y Bring scenes 4, 6, 8, 10, 16 to Level 3** · M each · Needs 1.10
- Add the scene's single interaction and its 20-second timeout.

**4.z Full playthrough again** · S · QA lead → **Milestone M4**

### Phase 5: Integrate delivered assets (Spring 2027, whenever they arrive)

**5.1 Check each delivery** · S per delivery · tech lead
- Import it into a test scene first. Check scale (1 unit = 1 m), that the rig is Humanoid, triangle and texture budgets, and frame rate on the Quest. Send problems back quickly.

**5.2 Drop in the two characters** · M · Needs 5.1
- Replace `Charbel_Placeholder` and `Monk_Placeholder` with the delivered models inside their prefabs. Because all of them are Humanoid, the Timelines keep working. Fix any clips that look wrong.

**5.3 Swap environments and props** · M per scene

**5.4 Real voice-over** · M per chapter
- Replace the placeholder clips in the VoiceLine assets. Line lengths will change, so re-time each Timeline so the action still matches what is said.

**5.5 Bake lighting** · M per scene
- Use baked lighting (fast on Quest). Use at most one real-time light.

**5.6 Full playthrough** · S · QA lead

### Phase 6: Captions and languages (Level 5)

**6.1 Localized text** · M · tech lead
- `LocalizedText` component: holds English, French and Arabic strings and shows the one for the chosen language. Set up fonts that cover French accents and Arabic.
- **Done when:** a sample text shows correctly in all three languages in the headset.

**6.2 Language choice and translated menus** · M · Needs 6.1, 2.8
- Add the language choice to `00_Welcome` (English / Français / العربية, selected by gaze) and store it in `AppState`.
- Switch the welcome text, chapter menu, credits and tradition label to `LocalizedText`.
- **Done when:** choosing a language changes every on-screen text.

**6.3 Caption display** · M · Needs 6.1
- `CaptionDisplay`: about 1.5 m in front, slightly below eye level, follows the head slowly, at most 2 lines. The Voice line track shows each line's caption for exactly its clip's length.
- **Done when:** a test scene shows synced captions in all three languages.

**6.4 Fill in the captions** · M per chapter · translators + scene owners · Needs 6.3, 2.8
- Paste each line's three translations into its VoiceLine asset.
- **Done when:** every voice line in every scene has its three caption texts.

**6.5 Caption check in the headset** · M · translators
- A speaker of each language watches the whole experience: spelling, timing, readability. → **Milestone M5**

### Phase 7: Polish and test

**7.1 Performance pass** · M · tech lead
- Run every scene with OVR Metrics Tool (Meta's on-headset frame-rate display) and fix any scene below budget (section 8).

**7.2 Audio mix** · M · audio
- Make narration loudness the same across all scenes, and balance music and ambience.

**7.3 Comfort and safety check** · S · QA lead
- No camera motion except during fades, no flashing faster than 3 times per second (important for the light over the grave and the locust scenes, because flashing can trigger seizures), and nothing sudden or too loud.

**7.4 Visitor playtests** · M · QA lead
- At least 10 people who are new to VR, including people with glasses and older visitors. Watch them; don't help unless needed. Note where they get confused.

**7.5 Bug fixing and content freeze** · L · whole team → **Milestone M6: content freeze (early June 2027)**

### Phase 8: Installation and operations (June 2027)

**8.1 Headsets** · M · tech lead
- Install the final build on every headset. Set the app to open automatically, or use a kiosk mode (check Meta's current options for managed or kiosk headsets). Turn off system notifications, and set brightness and volume.

**8.2 The space** · S · producer + library
- Fixed chairs that do not swivel, with free space around each one at least as big as the in-app boundary (1.2). Set up the Quest's own boundary (Guardian) on every headset for that space, and mark it on the floor. Enough light for hand tracking: it fails in the dark. No direct sunlight on the headsets, because sunlight through the lenses damages the screens.
- Built-in headset speakers may disturb the library. Decide on headphones with cleanable covers if needed.

**8.3 Hygiene and charging** · S · producer
- Washable face covers, wipes, and a lens cloth. A Quest 3 battery lasts about 2 hours, so plan a charging rotation or a charging station.

**8.4 Staff guide** · S · producer
- One page: how to start the app, put the headset on a visitor, clean it, charge it, and what to do if it freezes. Include photos.

**8.5 Dry run and fixes** · M → **Milestone M7: ready, 1 July 2027**

### Phase 9: Handover to the next cohort (every semester)

**9.1 Update the docs** · S · everyone
- Update the "how to build a scene" guide, this plan's scene list and statuses, a "known problems" list, and a short "decisions we made and why" log.
- **Done when:** a new student can open the project and build a Level 1 scene in their first week.

---

## 7. Roadmap and team

### Milestones

Dates assume Fall runs to late January and Spring runs February to June. Adjust them to the real academic calendar. The fixed point is **ready by 1 July 2027**.

| Milestone | What exists | When |
|---|---|---|
| M0 Setup | Everyone can build to a Quest (chunks 0.x) | November 2026 |
| M1 Vertical slice | Template finished + Scene 8 at Level 3 | End of Fall 2026 |
| M2 Content locked | Scene list, scene cards, script approved; asset requests sent | End of Fall 2026 |
| M3 First complete product | All 16 scenes at Level 1, full playthrough | Spring 2027, about week 4 |
| M4 Alive and interactive | All scenes Level 2, interactive scenes Level 3 | Spring 2027, about week 8 |
| M5 Delivered and captioned | Delivered characters and voices in; captions in 3 languages | Spring 2027, about week 13 |
| M6 Content freeze | Polished and tested | Early June 2027 |
| M7 Ready | Installed, staff trained, dry run done | 1 July 2027 |

If the schedule slips, keep finished scenes at a lower level rather than dropping milestones. Extra scenes stay at Level 1, or their story moves into a neighbouring scene's narration. If the delivered characters arrive late, the placeholders ship, so the product is still complete.

### Roles (one student can hold more than one)

| Role | Does |
|---|---|
| Producer | Runs the team board, meetings and deadlines; talks to Dr. Abbas and the library |
| Tech lead | Builds and maintains the template and scripts; reviews changes to shared prefabs; makes builds |
| Scene owners | Each owns 1 to 3 scenes from Level 1 to the end |
| Writer and translators | Script, caption translations, glossary |
| Audio lead | Mixer, placeholder voice-over, sounds, licences, final mix |
| Asset liaison | Asset requests and deliveries with the 3D faculty member |
| QA lead | Playthroughs, bug list, visitor playtests |

---

## 8. Rules and conventions

### Folders

Each shared system lives in its own folder with one owner, so students never edit the same files.

```text
Assets/
  _Project/
    Systems/<System>/        Scripts/, Prefabs/, a test scene and a README per system:
                             Rig, SceneFlow, Gaze, Interactions, VoiceLines,
                             SceneController, Characters, Audio
    Sandbox/<StudentName>/   personal test scenes, never part of the app
    Scenes/_Template/        the scene template
    Scenes/Main/             00_Welcome, 01_Menu, 99_Credits
    Scenes/Chapter1/ … Chapter4/   story scenes, each with its Timeline next to it
    Scenes/<scene name>/Scripts/   small scene-only scripts, if a scene needs one
    Environments/<scene>/    greybox now, delivered environment later
    Art/Placeholders/   Art/Delivered/
    Audio/VO/<scene>/        narration WAVs and their VoiceLine assets
    Audio/SFX/   Audio/Ambience/   Audio/Music/
  Docs/                      brief, plan, team guide, scene cards (Docs/scenes/)
```

How the team works with these folders on GitHub is in [team-guide.md](team-guide.md).

### Names

| Thing | Pattern | Example |
|---|---|---|
| Scene | `C<chapter>_S<scene 2 digits>_<Name>` | `C2_S08_TheLamp.unity` |
| Timeline | scene name + `_Timeline` | `C2_S08_TheLamp_Timeline.playable` |
| Voice audio | `VO_<scene>_<line 2 digits>` | `VO_C2_S08_03.wav` |
| VoiceLine asset | `VL_<scene>_<line>` | `VL_C2_S08_03.asset` |

### Git rules (avoid losing work)

1. **One owner per scene file.** Unity scene files are hard to merge, so never edit a scene you don't own. Claim it on the board.
2. **Shared systems** (`Systems/`) are changed only by each system's owner, with the tech lead's review.
3. **Pull before you start, and commit and push when you stop.** Write a message that says what changed ("C2_S08: added lamp interaction").
4. Never commit the `Library`, `Temp` or `Logs` folders (the `.gitignore` already handles this).
5. If Unity shows errors after pulling, tell the tech lead before changing anything.

### Definition of Done for a scene

Check what applies to the scene's current level:

- [ ] Plays start to end on a Quest 3 at a steady frame rate (target 72 fps or higher)
- [ ] Fades in and out
- [ ] Boundary set; stepping out fades the view, and no scene object is inside the boundary
- [ ] Narration plays in order and is clear over the music
- [ ] Tradition label shown if the scene is a devotional tradition
- [ ] The interaction (if any) works with hands and continues by itself after the timeout
- [ ] Duration within ±15 seconds of the target on its scene card
- [ ] No errors in the Console
- [ ] Follows the design rules (section 2)
- [ ] Level 5 only: captions correct and in sync in English, French and Arabic
- [ ] Played and checked by another student; scene card status updated

### Performance budget (Quest 3, starting values)

These are safe starting points. Measure with OVR Metrics Tool and adjust.

- Frame rate: 72 fps minimum (90 if possible)
- Draw calls: about 150 or fewer per frame
- Triangles in view: about 500,000 or fewer
- Lights: baked; at most 1 real-time light; no real-time shadows unless tested
- Textures: 2048 px or smaller for characters, 1024 px or smaller for props, ASTC compression
- No post-processing on Quest except what Meta's Project Setup Tool allows (HDR is off)

### Comfort and readability

- Captions: one language at a time, at most 2 lines and about 42 Latin characters per line, shown for at least 1.5 seconds. Test the size in the headset with someone wearing glasses.
- Text and menus about 1.5 to 2 m away, never closer than 0.5 m.
- No flashing faster than 3 times per second, and no sudden loud sounds.

### Audio

- Narration: 2D, clear and the same loudness in every scene. Source files as 48 kHz WAV.
- Sounds that come from objects (footsteps, a candle, a bell): 3D, placed on the object.
- Music and ambience: 2D, quieter under narration.
- Every file has a licence entry in `Docs/credits-and-licences.md`.

### Respect and accuracy

- Present facts with sources, and present traditions as traditions ("Tradition says…").
- His death, burial and exhumation are shown with dignity: no body close-ups, no decay.
- Charbel: eyes lowered, calm and slow movements, monastic habit.
- The audience includes people of all faiths and children.

---

## 9. Specs to send with asset requests

### The two characters we recommend asking for

With only two characters, choose ones that cover the most scenes:

1. **Charbel as a monk**, adult, in the black monastic habit. Used in scenes 5 to 12. If it is cheap for the 3D faculty member, a second texture or beard variant for the old hermit (Chapter III) helps, but it is optional; the narration can carry the passing years.
2. **A generic monk** that can be reused with small changes as Saint Nimatullah Al-Hardini, the two hermit uncles and the monks in the fields (scenes 3, 6, 7).

No child model is needed: Chapter I uses places, objects, light and silhouettes.

### Technical spec for 3D and voices

- **Format:** FBX. Scale 1 unit = 1 metre, real-world size, Y-up, faces forward along +Z.
- **Characters:** a humanoid skeleton in T-pose that Unity can map as **Humanoid**. 30,000 to 50,000 triangles each. Textures: base colour, normal and mask maps at up to 2048 px.
- **Animations:** separate, clearly named clips (for example `Charbel_Pray_Kneel`). Loops must loop cleanly. No root motion unless agreed. Both characters share the same Humanoid animations where possible.
- **Environments and props:** split into pieces we can place. Keep within the performance budget. Material and texture names should match the objects.
- **Props needed:** water-filled lamp, manuscripts and herbs, altar with an image of the Virgin, bed, candles, grave, coffin and tomb.
- **Voices:** narrator voice-over, 48 kHz WAV, one file per script line, named as in section 8. Clean room tone, no music.

### Name glossary (keep spellings the same everywhere)

| English | French | Arabic |
|---|---|---|
| Charbel Makhlouf | Charbel Makhlouf | شربل مخلوف |
| Youssef Antoun Makhlouf | Youssef Antoun Makhlouf | يوسف أنطون مخلوف |
| Bqaa Kafra | Bqaa Kafra | بقاعكفرا |
| Qadisha Valley | Vallée de la Qadisha | وادي قاديشا |
| Mayfouq | Mayfouq | ميفوق |
| Annaya | Annaya | عنّايا |
| Bkerke | Bkerké | بكركي |
| Saint Nimatullah Al-Hardini | Saint Nimatullah Al-Hardini | القديس نعمة الله الحرديني |

---

## 10. Scene card template

Copy this for every scene into `Docs/scenes/C<x>_S<yy>.md`.

```markdown
# Scene <number>: <title>
- Chapter / years:
- Owner:            Level: 1 / 2 / 3 / 4 / 5      Status:
- Target duration:  ___ s
- Story beat (1-2 sentences):
- Facts and sources:
- Devotional tradition label: yes / no
- Where the visitor sits and what they see in front:
- Figures: Charbel / Monk / silhouettes / none; what they do; animations needed:
- Interaction (if any): type, what happens, hint, timeout result:
- Narration: number of lines, link to script:
- Sounds: ambience / SFX / music:
- Assets needed (placeholder now → delivered later):
- Comfort notes (movement, light, loud sounds):
```

---

## 11. Open questions to settle

| # | Question | Why it matters | Ask |
|---|---|---|---|
| Q1 | Does the 16-scene list match the deck's beats? | Everything is built from it | Dr. Abbas |
| Q2 | Which language is the voice-over in? Is it a narrator only (assumed), or does Charbel speak too? | Script language; recordings | Dr. Abbas, voices faculty |
| Q3 | Will the 2 characters be the ones in section 9, and can they arrive by the end of March 2027? | Spring schedule | 3D faculty |
| Q4 | How many headsets and seats? Quest 3 or 3S? | Builds, throughput, budget | Dr. Abbas, library |
| Q5 | Kiosk or managed-headset setup available? | Running without staff touching a computer | IT / Meta options |
| Q6 | Who does the church and content review? | Approval before building content | Dr. Abbas |
| Q7 | Headset speakers or headphones in the library? | Noise for other library users; hygiene | Library Director |

**Settled:** narration is a voice-over with captions, and captions come after the scenes work. About 2 delivered characters arrive in Spring 2027. The product must be ready by 1 July 2027.

---

## 12. Risks and what we do about them

| Risk | What we do |
|---|---|
| Characters or voices arrive late in Spring | Placeholders first; ask for end of March; Level 1 to 3 scenes are a complete product without them |
| Only two characters | Chapter I without characters; one reusable generic monk; silhouettes and sound for everyone else |
| Real voice lines have different lengths | One file per line; re-timing a Timeline is quick (5.4); captions follow the audio automatically |
| Delivered assets are too heavy for Quest | Spec sent early (2.5); every delivery tested first (5.1) |
| A visitor stands up and walks into the chair, a wall or another visitor | Seated by default; in-app boundary fades the view; free space and Quest boundary set up around each seat (8.2) |
| Hand tracking fails (bad light, unusual hands) | Every interaction times out and continues; enough room light (8.2) |
| Too many scenes for the time | Core vs Extra priorities; build levels; Extra scenes can stay tableaux |
| New cohort each semester | Template, guide and handover (1.15, 9.1); small chunks |
| Students overwrite each other's work | One owner per scene; shared prefabs reviewed (section 8) |
| Not enough headsets for testing | Meta XR Simulator and Quest Link in the Editor (0.3) |
| Content judged inaccurate or insensitive | Sources per fact, tradition labels, church review (2.4) before building |
