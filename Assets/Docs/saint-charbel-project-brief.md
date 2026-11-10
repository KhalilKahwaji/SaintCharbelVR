# Saint Charbel Digital Heritage Center: Project Brief

## The project

A permanent installation in the USEK library, pitched to the President by Dr. Reine Abbas (Head of Digital Media Programs) and the Library Director.

- **Funding ask:** $14,900 external, against over $100k USEK in-kind contribution
- **Target opening:** his feast day, July 2027, the 50th anniversary of his canonization
- **Built by:** students, updated by each new cohort

## The visit: three stages, about 40 minutes

1. **The Journey:** four seated VR chapters on his life, 5 to 7 minutes each, 16 scenes, 26 minutes total. Hand tracking, gaze selection, chapter menu, captions in Arabic, French and English. Devotional traditions are labelled as such on screen.
2. **The Threshold:** a curtained, darkened room built between four library pillars. Nothing permanent.
3. **The Presence Room:** a life-sized Charbel projected on a scrim walks, turns toward visitors and prays, with spatial audio. Path B lets visitors walk around him in Quest 3 passthrough.
   - Year one: he walks and prays in silence
   - Year two: proximity-triggered prayers, recorded by students
   - Year three: a local AI model lets him respond

**Our part:** Stage 1, the VR chapters, built in the VR/AR course.

## Course context

- Students build the VR interaction layer: triggers, events, scene navigation, assembling delivered assets
- All 3D (character, rigging, voices) is delivered by another faculty member on request
- Each semester has a fresh cohort, so cohort one must leave a documented, reusable scene template (trigger wiring, event playback, dropping in the character) that the next cohort extends
- Extracurricular sessions can cover the time lost to teaching basics each semester

## Who builds it: students who are learning

The developers are students in the VR/AR course. Many are using Unity, C# and VR for the first time, and each semester a new cohort starts over. The project is designed around that:

- **Keep it as simple as possible.** Seated by default (visitors can choose to move, by teleporting inside each scene's boundary), scenes played by Unity Timeline, one shared scene template, at most one simple interaction per scene.
- **Nothing can get stuck.** Every interaction is optional and continues on its own after a timeout, because visitors are first-time VR users and students are first-time developers.
- **Placeholders first.** Every scene must work with greybox shapes, a placeholder Charbel and placeholder voice before the delivered 3D and voices arrive.
- **Always finished.** All 16 scenes are first built as simple narrated tableaux, then improved, so there is a complete experience at every milestone.
- **Small chunks.** Work is split into small tasks that each teach one skill and have a clear "done when" check.

The step-by-step plan is in [saint-charbel-development-plan.md](saint-charbel-development-plan.md).

---

## Key moments of his life, by chapter

### I. Childhood (1828–1851)

- **Birth.** Born Youssef Antoun Makhlouf on 8 May 1828 in Bqaa Kafra, the highest inhabited village in Lebanon (over 1,600 m), above the Qadisha Valley. The mountain landscape is a strong opening shot.
- **Loss of his father.** Youngest of five children. His father was taken for forced labor and died when Youssef was three.
- **The hermit uncles.** Two of his uncles lived as hermits, and he was drawn to their way of life and to the lives of the saints. Sets up the whole arc; not yet in the deck's beats.
- **The grotto.** He prayed alone in a cave above the village. The deck uses this as the chapter ending.

### II. The Monastery (1851–1875)

- **The secret departure.** At 23 he left home without telling his family, entering the Monastery of Our Lady in Mayfouq.
- **Taking the name.** Moved to Saint Maron in Annaya, took the name Charbel after an early martyr, solemn vows in 1853.
- **The teacher.** Studied under Saint Nimatullah Al-Hardini, who taught him manuscript work and herbal medicine. The scriptorium is a natural interactable scene.
- **Ordination.** Ordained a priest on 23 July 1859 at Bkerke.
- **Monastery life.** Sixteen years at Annaya (1859–1875), praying and working the fields.
- **The lamp (devotional tradition).** The lamp filled with water instead of oil that still burned. The deck has the visitor hold and raise it.

### III. The Hermitage (1875–1898)

- **Withdrawal.** In 1875 the abbot let him move to the Hermitage of Saints Peter and Paul near Annaya, where he lived his last 23 years.
- **Daily devotion.** He kept an image of the Virgin on the altar where he said Mass daily, and another beside his bed. Useful set dressing for the cell.
- **Humility.** Tradition says he never raised his eyes from the ground in front of others. Direct direction for the character animator.
- **The locusts (devotional tradition).** A witness testified that in 1885 a swarm of locusts darkened the sky over Annaya and the villages around it. Visually strong; not used in the deck.
- **The last Mass.** On 16 December 1898 he suffered a stroke while celebrating Mass. He died on 24 December 1898, aged 70.

### IV. What Followed (1898–today)

- **Burial.** Following monastic custom, buried unembalmed, in the full habit, directly in the ground with no coffin.
- **The light (devotional tradition).** Villagers reported a bright light over his grave for 45 nights. One account says the local Muslim prefect saw it and alerted church authorities.
- **The exhumation.** The body was reported incorrupt. On 15 April 1899 the Maronite Patriarch allowed it to be moved to a coffin in a new tomb inside the monastery.
- **1950, the crowds.** The tomb was opened again in 1950 and 1952, drawing large crowds.
- **Beatification and canonization.** Beatified 5 December 1965 and canonized 9 October 1977, both by Pope Paul VI.
- **Today.** Pilgrims of all faiths (Christians, Muslims, Druze) visit his tomb at Annaya. Pope Leo XIV visited the tomb on 1 December 2025, a possible closing beat.

---

## Open issues to settle

- **Engine mismatch.** The budget says Path B is built in Unreal Engine 5. The VR course teaches Unity with the Meta SDK. RESOLVED: WILL USE UNITY6
- **Who makes the 3D.** The deck says every asset is student-made; in practice another faculty member delivers the 3D. Align with Dr. Abbas on what the deck promises.
- **Department count.** One-pager says 9 departments; deck and budget say 3.
- **Scope vs timeline.** 16 scenes by July 2027 with a fresh cohort each semester is a lot. The reusable template is what makes it feasible.
- **Narration.** RESOLVED: narrator voice-over with captions in Arabic, French and English. Scenes are built without captions first; captions are added last. Still open: which language the voice-over is in.
- **Characters.** RESOLVED: about 2 delivered characters, expected in the Spring 2027 semester. Placeholders and silhouettes cover everything else.
- **Deadline.** RESOLVED: ready by 1 July 2027.

## Sources

- https://en.wikipedia.org/wiki/Charbel_Makhlouf
- https://vaticanstate.va/en/state-and-government/general-informations/saint-of-the-day/2303-july-24-saint-charbel-makhlouf.html
- https://angelusnews.com/faith/saint-of-the-day/charbel-makhlouf/
- https://catholicnovenaapp.com/saints/about-st-charbel/
- https://www.viaperegrina.org/sites/monastery-of-st-maron-and-shrine-of-st-charbel-annaya
- https://pilgrimcenterofhope.org/file_download/c1ced18f-84ae-4aef-9435-6a72540538f2
- USEK President deck, one-pager and budget summary (internal)
