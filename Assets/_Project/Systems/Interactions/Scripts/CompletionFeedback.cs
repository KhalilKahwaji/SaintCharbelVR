using System.Collections;
using UnityEngine;

namespace SaintCharbel
{
    // A soft glow and a small sound when a scene action is completed.
    // Put it on the same object as a TouchTrigger or RaiseTrigger, then in that trigger's
    // On Completed event add this object and choose CompletionFeedback.Play.
    public class CompletionFeedback : MonoBehaviour
    {
        [Tooltip("The object's renderer that glows. Leave empty to use the one on this object.")]
        public Renderer glowRenderer;

        [Tooltip("The colour of the glow. A warm, soft colour fits best.")]
        public Color glowColor = new Color(1f, 0.85f, 0.5f);

        [Tooltip("How bright the glow gets at its peak (1 = soft, 3 = strong).")]
        public float glowStrength = 1.5f;

        [Tooltip("How long (in seconds) the glow takes to fade in and back out.")]
        public float glowDuration = 1.5f;

        [Tooltip("Plays the sound. Its Output should be the SFX group of MainMixer.")]
        public AudioSource audioSource; // TODO: set Output to MainMixer > SFX when Group 4's mixer is merged

        [Tooltip("The small sound played on completion, for example a soft chime.")]
        public AudioClip sound;

        private Material glowMaterial;

        void Start()
        {
            if (glowRenderer == null)
            {
                glowRenderer = GetComponent<Renderer>();
            }

            if (glowRenderer != null)
            {
                // .material gives this object its own copy, so other objects using the same material don't glow.
                glowMaterial = glowRenderer.material;
                // URP Lit only shows emission when this keyword is on.
                glowMaterial.EnableKeyword("_EMISSION");
                glowMaterial.SetColor("_EmissionColor", Color.black);
            }
        }

        // Connect this to the trigger's On Completed event.
        [ContextMenu("Play")]
        public void Play()
        {
            if (audioSource != null && sound != null)
            {
                audioSource.PlayOneShot(sound);
            }

            if (glowMaterial != null)
            {
                StopAllCoroutines();
                StartCoroutine(Glow());
            }
        }

        private IEnumerator Glow()
        {
            float time = 0f;
            while (time < glowDuration)
            {
                time += Time.deltaTime;
                // Goes 0 → 1 → 0 over the duration, so the glow rises and fades softly.
                float amount = Mathf.Sin(Mathf.PI * time / glowDuration);
                glowMaterial.SetColor("_EmissionColor", glowColor * glowStrength * amount);
                yield return null;
            }

            glowMaterial.SetColor("_EmissionColor", Color.black);
        }
    }
}
