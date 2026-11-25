# Audio

**Owner:** Group 4 (Objects, Characters and Sound) · Tasks: [semester1-tasks.md](../../../Docs/semester1-tasks.md), section 5

Contains: `MainMixer` and the four ready-made audio objects in `Prefabs/`.

## My plan

- **Sound setup (session 2):** `MainMixer` with Narration, Music, Ambience and SFX groups; music and ambience get quieter under the narration; four audio objects.
- Sound files live in `Assets/_Project/Audio/` (Ambience, SFX, Music, VO) and every one is listed in `Assets/Docs/credits-and-licences.md`.

## What it does

Every sound in the app goes through `MainMixer`, so we can set the volume of each kind of sound in one place, and **music and ambience automatically get quieter while the narration plays**.

| Group | For | Ducks under narration? |
|---|---|---|
| Narration | The voice | (it is the one that ducks the others) |
| Music | Background music | Yes |
| Ambience | Wind, birds, candle… | Yes |
| SFX | Short sounds from objects (bell, chime, footsteps) | No |

How the ducking works: the Narration group has two **Send** effects (0 dB) going to a **Duck Volume** effect on Music and on Ambience. When the voice is louder than the Threshold, Duck Volume turns that group down.
Duck Volume settings: Threshold −30 dB, Ratio 400 %, Attack 100 ms, Release 600 ms.

## How to use it

Drag one of these from `Prefabs/` into your scene and put a clip in its Audio Source:

| Prefab | Output | Heard | Loop | Play On Awake |
|---|---|---|---|---|
| `[Audio] Narration` | Narration | the same everywhere (2D) | no | no |
| `[Audio] Music` | Music | the same everywhere (2D) | yes | yes |
| `[Audio] Ambience` | Ambience | the same everywhere (2D) | yes | yes |
| `[Audio] SFX` | SFX | from where the object is (3D) | no | no |

- **Group 3's Scene Director:** drag your scene's `[Audio] Narration` into **Narration Source** and `[Audio] Ambience` into **Ambience Source**.
- **Any other Audio Source** (for example on a TouchTrigger with `CompletionFeedback`): set its **Output** to `MainMixer > SFX`.
- To change how much the music drops, open `MainMixer` (Window → Audio → Audio Mixer), click Music or Ambience, and change the Duck Volume **Threshold** (lower = ducks sooner) or **Ratio** (higher = ducks more).

## Known problems

- Placeholder sounds (birds, footsteps) are simple generated sounds; replace them with real recordings when we have them, and update the credits.
- The duck only reacts to sound that actually plays through the Narration group: a narration Audio Source with Output left empty won't duck anything.
