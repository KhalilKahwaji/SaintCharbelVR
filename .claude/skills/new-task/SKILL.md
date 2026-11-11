---
name: new-task
description: Start a new task on the Saint Charbel VR project - pull the latest main and create a correctly named branch (firstname/task). Use when the student starts a session, starts a new task, or asks how to make or name a branch.
argument-hint: "[short task description, e.g. 'fader' or 'scene 8']"
---

# Start a new task

Task description from the student (may be empty): $ARGUMENTS

## 1. Know the student

Read `CLAUDE.local.md`. If it's missing, stop and ask them to run `/setup` first. Take the **branch prefix** from it.

## 2. Make sure nothing gets lost

Run `git status --short` and `git branch --show-current`.

- **Uncommitted changes?** Don't stash, discard or carry them over silently. Show the list and ask:
  - if they belong to the current task: finish that first with `/finish-task`;
  - if they're Unity noise (only `ProjectSettings/`, `Packages/` or files outside their area): explain, and discard them only after the student says yes (`git restore <file>`).
- **In the middle of a merge or rebase?** Stop and tell them to call the instructor.

## 3. Get the latest `main`

```text
git switch main
git pull origin main
```

Explain: "This is the same as choosing `main` in GitHub Desktop, then Fetch origin and Pull origin." If Unity is open, it will reimport new files; that's normal.

If the pull fails or reports a conflict, stop and tell the student to call the instructor.

## 4. Choose the branch name

If the task is unclear, ask what they're going to do (one sentence) and look it up in their group's section of `Assets/Docs/semester1-tasks.md`.

Rules: `<prefix>/<task>`, all lowercase, ASCII letters, digits and hyphens only, 1 to 3 words, no spaces, dates or group numbers.

| Kind of task | Pattern | Example |
|---|---|---|
| First task (session 1) | `<prefix>/setup` | `maya/setup` |
| Starter task | `<prefix>/starter-<thing>` | `maya/starter-welcome-panel` |
| A system part | `<prefix>/<part>` | `maya/fader`, `rami/raise-trigger`, `lea/chapter-menu` |
| A story scene | `<prefix>/c<chapter>-s<scene 2 digits>` | `rami/c2-s08` (Chapter II, scene 8) |
| A scene card (narration text) | `<prefix>/c<chapter>-s<scene>-card` | `rami/c2-s08-card` |
| Placeholder narration audio | `<prefix>/c<chapter>-s<scene>-vo` | `rami/c2-s08-vo` |
| Fixing something | `<prefix>/fix-<what>` | `maya/fix-fader-flash` |
| System README | `<prefix>/<system>-readme` | `maya/sceneflow-readme` |

Chapters: I = 1 (scenes 1-4), II = 2 (scenes 5-8), III = 3 (scenes 9-12), IV = 4 (scenes 13-16).

Check the name isn't taken: `git branch --list <name>` and `git ls-remote --heads origin <name>`. If it exists:
- it's their own unfinished branch for the same task: offer to switch to it instead (`git switch <name>`, then `git pull`);
- otherwise add a suffix: `maya/fader-2`.

Tell the student the name and why, and confirm before creating it.

## 5. Create the branch

```text
git switch -c <name>
```

Explain: "Same as Branch → New branch in GitHub Desktop, based on `main`." The branch is published to GitHub the first time they push (with `/finish-task`).

## 6. Get them started

In a few lines:

- The branch they're on.
- The files they may change for this task (from `CLAUDE.local.md`). Remind them that for a story scene they only open **their own** scene.
- The task's steps and its **How to test** checklist from the tasks file. Point out any 🟠 part ("call the instructor for this one").
- Offer to start with the first small step.
