using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace SaintCharbel
{
    // A scene action: completes when the visitor lifts this object a set height
    // (25 cm by default) above where it started. Used for Scene 8, The Lamp.
    public class RaiseTrigger : MonoBehaviour
    {
        [Tooltip("How high (in metres) the object must be lifted to complete.")]
        public float raiseHeight = 0.25f;

        [Tooltip("Optional: a small hand symbol near the object. Hidden at start, shown by ShowHint().")]
        public GameObject hint;

        [Tooltip("How long (in seconds) ForceComplete takes to lift the object by itself.")]
        public float forceRaiseDuration = 2f;

        [Tooltip("Called once, when the object has been raised.")]
        public UnityEvent OnCompleted;

        private float startHeight;
        private bool completed;

        void Start()
        {
            startHeight = transform.position.y;

            if (hint != null)
            {
                hint.SetActive(false);
            }
        }

        void Update()
        {
            if (completed)
            {
                return;
            }

            if (transform.position.y - startHeight >= raiseHeight)
            {
                Complete();
            }
        }

        // Shows the hand symbol, to tell the visitor what to do.
        [ContextMenu("Show Hint")]
        public void ShowHint()
        {
            if (completed)
            {
                Debug.Log(name + ": no hint, the raise is already done.");
                return;
            }

            if (hint == null)
            {
                Debug.LogWarning(name + ": ShowHint was called but no Hint object is set in the Inspector.");
                return;
            }

            hint.SetActive(true);
        }

        // The action happens by itself: the object rises smoothly, then completes.
        [ContextMenu("Force Complete")]
        public void ForceComplete()
        {
            if (completed)
            {
                return;
            }

            StartCoroutine(RiseByItself());
        }

        private IEnumerator RiseByItself()
        {
            // Stop gravity from pulling the object back down while it rises.
            Rigidbody body = GetComponent<Rigidbody>();
            if (body != null)
            {
                body.isKinematic = true;
            }

            Vector3 from = transform.position;
            Vector3 to = new Vector3(from.x, startHeight + raiseHeight, from.z);

            float time = 0f;
            while (time < forceRaiseDuration)
            {
                time += Time.deltaTime;
                float t = Mathf.SmoothStep(0f, 1f, time / forceRaiseDuration);
                transform.position = Vector3.Lerp(from, to, t);
                yield return null;
            }

            transform.position = to;
            Complete();
        }

        private void Complete()
        {
            if (completed)
            {
                return;
            }

            completed = true;

            if (hint != null)
            {
                hint.SetActive(false);
            }

            Debug.Log("Raised!");
            OnCompleted.Invoke();
        }
    }
}
