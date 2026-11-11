---
name: finish-task
description: Save a student's work on the Saint Charbel VR project - check the changed files, commit only their own files with their .meta files, push the branch, and help open the pull request. Use at the end of a session, when a task works, or when the student asks to commit, push or open a pull request.
---

# Commit, push and open the pull request

## 1. Check where we are

Read `CLAUDE.local.md` (missing → ask them to run `/setup`). Then `git branch --show-current`.

**On `main`?** Never commit there. Create the right branch first, keeping their changes: `git switch -c <prefix>/<task>` (naming rules in `/new-task`). Explain that their changes came along.

Ask the student to **save in Unity first**: Ctrl+S for the scene and File → Save Project for assets. Unity only writes changes to disk when it saves.

## 2. Sort the changed files

Run `git status --short --untracked-files=all` and put every file in one of three groups. Show them as a short list:

1. **Mine**: inside the folders listed in `CLAUDE.local.md`. These get committed.
2. **Unity noise**: `ProjectSettings/`, `Packages/`, `UserSettings/`, `Assets/Resources/` settings assets, or anything else Unity changed on its own. Not committed.
3. **Someone else's**: another group's folder or another person's scene. Not committed.

For groups 2 and 3, explain in one line why, and ask before discarding them (`git restore <file>` for changed files; for new untracked files just leave them out of the commit, don't delete them). If the student says a file outside their area really is needed, stop: they must ask the instructor first.

## 3. Check the `.meta` files

For every new or moved file under `Assets/` in group 1:

- `Foo.cs` needs `Foo.cs.meta`; a new folder `Bar/` needs `Bar.meta`.
- A `.meta` without its file (or the opposite) means something was deleted or moved outside Unity.

If any `.meta` is missing, ask them to switch to Unity, wait for it to import, and run `/finish-task` again. Never write `.meta` files yourself.

## 4. Quick quality check

Ask (don't block on it):
- Does the project open with **no red errors** in the Console?
- Did they test it in the headset or the Meta XR Simulator?
- Can they explain every script in this commit? For any C# file in the commit, ask **one** short question about it (for example "What happens if `nextScene` is empty?"). If they can't answer, explain it now: the instructor may ask in the demo.

## 5. Commit

Stage only the group 1 files by name (`git add <file> <file.meta> ...`). Never use `git add -A` or `git add .`.

Suggest a message: short, starting with the part's name, saying what changed. For example `Fader: fade in on start`, `C2_S08: greybox and narration`, `RaiseTrigger: fire once at 25 cm`. Let the student change it, then `git commit -m "<message>"`.

## 6. Push

`git push -u origin <branch>`. Explain: "Same as Push origin (or Publish branch) in GitHub Desktop."

If the push is rejected, don't force it. Run `git pull origin <branch>` once; if that gives a conflict, stop and tell them to call the instructor.

## 7. Pull request

If the task is done, or it's the end of the session:

- If the `gh` command is installed and logged in (`gh auth status`), offer `gh pr create --base main --fill`, then help them tick the checklist from `.github/pull_request_template.md` honestly.
- Otherwise: in GitHub Desktop, click **Create Pull Request**. Or open `https://github.com/<owner>/<repo>/compare/main...<branch>?expand=1` (build it from `git remote get-url origin`).

Remind them: fill in "What I did" in one or two sentences and tick only the checklist items that are true. The instructor reviews and merges it. Never merge it yourself.

## 8. What's next

One line: for the next task, run `/new-task` (it switches to `main`, pulls, and makes a new branch). Don't keep working on this branch after its pull request is merged.
