using UnityEngine;
using UnityEngine.Events;

namespace SaintCharbel
{
    // A scene action: completes when the visitor has looked at this object for
    // 2 seconds. Works like Group 4's TouchTrigger and RaiseTrigger (same names:
    // OnCompleted, ShowHint(), ForceComplete()), so the Scene Director can use
    // all three the same way.
    // Needs a GazeTarget on the same object (it is added automatically).
    [RequireComponent(typeof(GazeTarget))]
    public class GazeTrigger : MonoBehaviour
    {
        [Tooltip("Optional: a small eye symbol near the object. Hidden at start, shown by ShowHint().")]
        public GameObject hint;

        [Tooltip("Called once, when the object has been looked at long enough.")]
        public UnityEvent OnCompleted;

        private bool completed;

        void Start()
        {
            // When our GazeTarget is selected, call Complete().
            GetComponent<GazeTarget>().OnSelected.AddListener(Complete);

            if (hint != null)
            {
                hint.SetActive(false);
            }
        }

        // Shows the eye symbol, to tell the visitor what to do.
        [ContextMenu("Show Hint")]
        public void ShowHint()
        {
            if (completed)
            {
                Debug.Log(name + ": no hint, the look is already done.");
                return;
            }

            if (hint == null)
            {
                Debug.LogWarning(name + ": ShowHint was called but no Hint object is set in the Inspector.");
                return;
            }

            hint.SetActive(true);
        }

        // The action happens by itself, as if the visitor had looked at the object.
        [ContextMenu("Force Complete")]
        public void ForceComplete()
        {
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

            Debug.Log("Looked at: " + name + " (GazeTrigger completed)");
            OnCompleted.Invoke();
        }
    }
}
