# Team Guide: How We Work Together

Eight people work on one Unity project. This page explains how we do that without losing work. It's short on purpose; read all of it once.

Your tasks are in [semester1-tasks.md](semester1-tasks.md). The full project plan is in [saint-charbel-development-plan.md](saint-charbel-development-plan.md).

## The 5 golden rules

1. **Only change your group's folders, your own sandbox, and your own scenes.** Nothing else.
2. **Never change ProjectSettings, Packages or the Build Profiles scene list.** If you need a package or a setting, ask the instructor.
3. **Always commit the `.meta` file together with its asset.** Every file in Unity has a `.meta` partner. Without it, the asset breaks for everyone else.
4. **Pull before you start. Push before you leave.**
5. **`main` must always open without errors.** If it doesn't, tell the instructor right away. Don't try to fix other people's files.

Why so strict? Unity scene and prefab files are very hard to merge when two people change the same one. If everyone stays in their own files, merge conflicts almost never happen.

---

## One-time setup (before session 1)

Do this at home, because the downloads are big (about 10 GB).

1. **Unity:** install Unity Hub, then Unity **6000.3.16f1** (this exact version) with the **Android Build Support** module (tick OpenJDK and Android SDK & NDK too).
2. **GitHub Desktop:** install it from desktop.github.com and sign in with your GitHub account. Send your GitHub username to the instructor.
3. **Accept the invite:** you'll get an email from GitHub inviting you to the `SaintCharbelVR` repository. Accept it.
4. **Clone:** in GitHub Desktop, choose File → Clone repository → `SaintCharbelVR`. Choose a short local path such as `D:\Unity\SaintCharbelVR`.
   - **Not** inside OneDrive, Dropbox or Google Drive. Syncing folders break Unity projects.
   - If GitHub Desktop asks to initialize Git LFS, click **Yes**.
5. **Open it once:** Unity Hub → Add → Add project from disk → choose the folder. The first open takes 10 to 30 minutes. That's normal: Unity builds its `Library` folder.
6. **Check:** open `Assets/Scenes/SampleScene` and press Play. If you see red errors in the Console, send a screenshot to the instructor before session 1.

---

## Every session: the loop

```text
1. Pull main  →  2. New branch  →  3. Work in your files  →  4. Commit + push  →  5. Pull request  →  6. Instructor merges
```

### 1. Pull `main`

In GitHub Desktop, set **Current branch** to `main`, then click **Fetch origin**, then **Pull origin**.

Close Unity before pulling if you can. If Unity is open, it will reimport the new files, which is fine, just slower.

### 2. Make a branch for your task

**Branch → New branch**, based on `main`. Name it `yourname/what-you-do`, for example `maya/fader` or `rami/c2-s08`. One branch per task.

### 3. Work only in your files

Save often in Unity (Ctrl+S saves the scene; File → Save Project saves assets).

### 4. Commit and push

In GitHub Desktop, look at the list of changed files **before** you commit.

- **Only your files?** Good. Write a short message that says what changed, for example "Fader: fade in on start", then click **Commit**, then **Push origin**.
- **Files you didn't mean to change** (ProjectSettings, Packages, someone else's scene)? Right-click them → **Discard changes**. Unity sometimes touches these files on its own. If you aren't sure, ask.

Commit small and often: at least once per session, and every time something works.

### 5. Open a pull request

When your task is done, or at the end of each session: click **Create Pull Request** in GitHub Desktop. It opens GitHub in your browser. Fill in the checklist there and click **Create pull request**.

### 6. The instructor merges it

The instructor checks it and merges it into `main`. Then everyone gets your work at their next pull.

For your next task, go back to step 1: switch to `main`, pull, make a new branch.

### Switching branches

Save and commit before switching. If Unity shows strange errors after a switch, close Unity and open it again.

---

## When something goes wrong

| Problem | What to do |
|---|---|
| GitHub Desktop says **conflict** | Stop. Don't choose anything. Call the instructor. It usually means a file outside your folder was changed. |
| Red errors in the Console after pulling | Don't fix other people's code. Tell the instructor and the file's owner. |
| You committed something by mistake (not pushed yet) | **History** tab → right-click the commit → **Undo commit**. |
| You pushed something by mistake | Tell the instructor. Nothing is lost; Git keeps all history. |
| You deleted or broke your own file | Right-click it in GitHub Desktop → **Discard changes** to get the last committed version back. |
| Pink materials | The material uses a shader that isn't URP. Use a URP Lit material. |
| You need another group's part that isn't merged yet | Use a simple stand-in (see "Names everyone must use" in the tasks file) and mark it with `// TODO`. |
| You're stuck | Use the 20-minute rule (below). |

## The 20-minute rule

If you're stuck for 20 minutes:

1. Ask your AI tool, with the exact error message and what you tried.
2. Still stuck? Ask your partner.
3. Still stuck? Ask the group whose part you're using.
4. Still stuck? Ask the instructor.

Being stuck is normal; staying stuck alone is not.

---

## Using AI

You may use any AI tool (Claude, ChatGPT, Copilot, Gemini…) to write code, explain things and find bugs. That's how we can do this much in 2-hour sessions. But:

- **You must understand what you commit.** In the demo, the instructor may point at a line and ask what it does. "The AI wrote it" isn't an answer.
- **Test it in the headset or the Simulator.** The AI can't see your project; it often sounds sure and is wrong.
- **Give it the project context first** (below), or it will guess the wrong tools.
- **Work in small steps.** Ask for one script at a time, test it, then ask for the next part.
- **Paste the exact error** from the Console, not a description of it.
- **If an AI tool edits files for you** (Copilot, Cursor, Claude Code), check the changed files in GitHub Desktop before committing. It must only touch your folder.

### Paste this at the start of every AI chat

```text
I'm a student building a Unity VR project. Context:

- Unity 6000.3.16f1, Universal Render Pipeline (URP).
- Meta XR SDK v207 (All-in-One) with OpenXR, target Meta Quest 3, hand tracking.
- For interactions we use Meta's Interaction SDK and Building Blocks. We do NOT use XR Interaction Toolkit.
- The visitor is seated by default. If they choose "Move around", they can teleport and snap turn, but only inside the scene's boundary. No smooth movement.
- Scenes are played by Unity Timeline.
- C# namespace: SaintCharbel. My files go in: Assets/_Project/Systems/<OurFolder>/
- My task: <paste your task from the tasks file>
- Names I must use exactly: <paste the rows you need from "Names everyone must use">
Help me step by step. Keep the code simple, and explain each script in a few sentences so I can explain it in class.
```

---

## Where things go

```text
Assets/_Project/
  Systems/                 the shared parts, one owning group each
    Rig/ (Group 1)   SceneFlow/ and Gaze/ (Group 2)   SceneController/ (Group 3)
    Interactions/, Characters/ and Audio/ (Group 4)
      Scripts/  Prefabs/  <System>_Test.unity  README.md
  Sandbox/<YourName>/      your own test scenes; never part of the app
  Scenes/
    _Template/             the scene template (instructor)
    Main/                  00_Welcome, 01_Menu and 99_Credits (Group 2)
    Chapter1/ … Chapter4/  story scenes, one owner per scene
  Environments/<scene>/    greybox pieces for each scene (scene owner)
  Audio/VO/<scene>/        placeholder narration (scene owner)
  Audio/SFX/  Audio/Ambience/  Audio/Music/   (Group 4)
Assets/Docs/               plan, brief, this guide, scene cards (Docs/scenes/)
```

### File names

| Thing | Pattern | Example |
|---|---|---|
| Story scene | `C<chapter>_S<scene 2 digits>_<Name>` | `C2_S08_TheLamp.unity` |
| Narration audio | `VO_<chapter+scene>_<line 2 digits>` | `VO_C2_S08_03.wav` |
| Voice line asset | `VL_<chapter+scene>_<line 2 digits>` | `VL_C2_S08_03.asset` |
| Branch | `yourname/task` | `maya/fader` |

### Your system's README

Every folder in `Systems/` has a `README.md`, written by the group that owns it. By the end of session 3 it must answer:

1. **What it does**, in one sentence.
2. **How to use it**, in 3 to 6 steps (screenshots help).
3. **Known problems.**

The next cohort will read it, so write it for someone who has never seen your code.
