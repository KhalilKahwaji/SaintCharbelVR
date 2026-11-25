using System.Collections;
using UnityEngine;

namespace SaintCharbel
{
    // A small, calm "Devotional tradition" sign. Scenes turn it on when the narration
    // tells a tradition (for example "Tradition says the lamp burned with water").
    // Call Show() and Hide() from a Timeline Signal or a UnityEvent.
    [RequireComponent(typeof(CanvasGroup))]
    public class TraditionLabel : MonoBehaviour
    {
        [Tooltip("How long (in seconds) the label takes to fade in or out. Slow is calmer.")]
        public float fadeDuration = 1.5f;

        [Tooltip("Tick to show the label as soon as the scene starts.")]
        public bool showOnStart = false;

        private CanvasGroup group;

        void Awake()
        {
            // The CanvasGroup's Alpha makes the whole label (background and text) see-through.
            group = GetComponent<CanvasGroup>();
            group.alpha = showOnStart ? 1f : 0f;
        }

        [ContextMenu("Show")]
        public void Show()
        {
            FadeTo(1f);
        }

        [ContextMenu("Hide")]
        public void Hide()
        {
            FadeTo(0f);
        }

        // Show it if hidden, hide it if shown. Handy for testing.
        public void Toggle()
        {
            FadeTo(group.alpha > 0.5f ? 0f : 1f);
        }

        private void FadeTo(float target)
        {
            // Stop a fade that is still running, so Show then quickly Hide doesn't fight.
            StopAllCoroutines();
            StartCoroutine(Fade(target));
        }

        private IEnumerator Fade(float target)
        {
            float start = group.alpha;
            float time = 0f;
            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                group.alpha = Mathf.Lerp(start, target, time / fadeDuration);
                yield return null;
            }

            group.alpha = target;
        }
    }
}
