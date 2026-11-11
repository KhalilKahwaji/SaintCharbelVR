---
name: setup
description: One-time setup for a new student on the Saint Charbel VR project. Asks their name and group, checks Git and Git LFS, and writes CLAUDE.local.md so Claude knows who they are and which files are theirs. Use when CLAUDE.local.md is missing or the student asks to set up, change group, or record their scenes.
---

# Set up Claude Code for a student

Goal: at the end, `CLAUDE.local.md` exists at the project root and says who the student is and exactly which files they may change. Be friendly and brief; this student may be new to the terminal.

## 1. Check the tools

Run these and report problems in plain words (don't fix global settings without asking):

- `git --version`. If missing: they need **Git for Windows** (git-scm.com). Claude Code needs it too.
- `git lfs version`. If missing, or if `git lfs env` shows no hooks: run `git lfs install` (safe, user-level).
- `git remote get-url origin` should point to the `SaintCharbelVR` repository. If not, they're in the wrong folder: stop and say so.
- `git config user.name` and `git config user.email`. If either is empty, ask for their name and the email of their GitHub account and set them with `git config --global`. (GitHub Desktop usually sets these already.)
- The project path: if it's inside OneDrive, Dropbox or Google Drive, warn them that this breaks Unity projects and point them to the team guide's one-time setup.

## 2. Ask who they are

If `CLAUDE.local.md` already exists, read it and ask only what they want to change.

Otherwise ask (use the AskUserQuestion tool where it fits):

1. **First name**, as they want it in branch names and their sandbox folder. Branch prefix = lowercase, ASCII letters only (for example `Maya` → `maya`, `Rami-Jo` → `ramijo`). Folder name keeps the capital: `Maya`.
2. **Group**: 1 The Visitor, 2 Screens and Navigation, 3 Story Player, 4 Objects, Characters and Sound.
3. **Person 1 or Person 2** in the group (or "not decided yet").
4. **Their scenes**, only if they already know them (from session 4 on). Otherwise "not chosen yet".

## 3. Write `CLAUDE.local.md`

Use this table to fill in the group's details:

| Group | Name | System folders (under `Assets/_Project/`) | Other files they own | Chapter | Scene folder | Must scenes | If time |
|---|---|---|---|---|---|---|---|
| 1 | The Visitor | `Systems/Rig/` | | III. The Hermitage | `Scenes/Chapter3/` | 10 The cell, 12 The last Mass | 9, 11 |
| 2 | Screens and Navigation | `Systems/SceneFlow/`, `Systems/Gaze/` | `Scenes/Main/` (00_Welcome, 01_Menu, 99_Credits) | I. Childhood | `Scenes/Chapter1/` | 1 The mountain, 4 The grotto | 2, 3 |
| 3 | Story Player | `Systems/SceneController/` | the scene template in `Scenes/_Template/` (only with the instructor) | IV. What Followed | `Scenes/Chapter4/` | 13 Burial, and the light, 16 Annaya today | 14, 15 |
| 4 | Objects, Characters and Sound | `Systems/Interactions/`, `Systems/Characters/`, `Systems/Audio/` | `Audio/SFX/`, `Audio/Ambience/`, `Audio/Music/`, `Assets/Docs/credits-and-licences.md` | II. The Monastery | `Scenes/Chapter2/` | 8 The lamp, 5 Leaving home, a new name | 6, 7 |

Write the file with this shape (fill in real values; keep it short):

```markdown
# About me (personal: not committed)

- Name: Maya
- Branch prefix: `maya/` (for example `maya/fader`)
- Group: 2, Screens and Navigation. I'm Person 1.
- Partner's work and mine are both in our group's folders; we test each other's work.
- Chapter: I. Childhood

## Files I may change

- `Assets/_Project/Sandbox/Maya/` (my sandbox; never part of the app)
- `Assets/_Project/Systems/SceneFlow/` and `Assets/_Project/Systems/Gaze/` (my group's systems)
- `Assets/_Project/Scenes/Main/` (00_Welcome, 01_Menu, 99_Credits)
- My scenes: not chosen yet. When chosen, also: the scene file in `Assets/_Project/Scenes/Chapter1/`, `Assets/_Project/Environments/<scene>/`, `Assets/_Project/Audio/VO/<scene>/` and `Assets/Docs/scenes/C1_S<yy>.md`

Everything else is read-only for me. If a change is needed elsewhere, I ask the owning group or the instructor.

## My tasks

See my group's section in `Assets/Docs/semester1-tasks.md` (section 5, Group 2, Person 1).
```

Then confirm `CLAUDE.local.md` is ignored by Git: `git check-ignore CLAUDE.local.md` must print the file name. If it doesn't, tell the student to ask the instructor (don't edit `.gitignore` yourself).

## 3b. Turn on the safety rules

Copy `.claude/skills/setup/student-settings.local.json` to `.claude/settings.local.json` (personal, not committed). If `.claude/settings.local.json` already exists, merge: keep its existing entries and add every rule from the `deny` list. Check `git check-ignore .claude/settings.local.json` prints the file name.

Tell the student in one line what it does: Claude Code can no longer change `ProjectSettings/` or `Packages/`, push to `main`, force-push, or erase work, even by accident. These rules take effect right away; if they don't seem to, restart Claude Code.

## 4. Check where they are

- `git branch --show-current` and `git status --short`. If they have uncommitted changes, mention them; don't touch them.
- If `Assets/_Project/Sandbox/<Name>/` doesn't exist yet, their first task is session 1, step 2: make a branch `<prefix>/setup`, create the sandbox folder and an empty scene `<Name>_Sandbox.unity` **in Unity** (so the `.meta` files are made), then commit and open a pull request. Offer to start it with `/new-task`.

## 5. Finish

Tell them in a few lines:

- What you now know about them (name, group, folders).
- The loop every session: `/new-task` at the start, work, `/finish-task` at the end. The same steps work by hand in GitHub Desktop (team guide, "Every session: the loop").
- Always check the changed files before committing, and understand every line you commit.
- They can run `/setup` again any time (for example in session 4, to add their scenes).
