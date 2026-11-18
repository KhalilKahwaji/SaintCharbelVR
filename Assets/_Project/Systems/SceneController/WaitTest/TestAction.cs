using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace SaintCharbel
{
    // TEST ONLY: a stand-in for GazeTrigger / TouchTrigger / RaiseTrigger (Groups 2 and 4).
    // TODO: replace with a real trigger once they are merged.
    // Press Space to "do the action". It has the same names as the real triggers.
    public class TestAction : MonoBehaviour
    {
        [Tooltip("Shown by ShowHint(), hidden when the action is done. E.g. a small sphere.")]
        public GameObject hint;

        public UnityEvent OnCompleted;

        private bool done;

        private void Start()
        {
            if (hint != null) hint.SetActive(false);
        }

        private void Update()
        {
            if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Complete("pressed Space");
            }
        }

        public void ShowHint()
        {
            Debug.Log("TestAction: hint shown. Press Space, or wait.", this);
            if (hint != null) hint.SetActive(true);
        }

        public void ForceComplete()
        {
            Complete("timed out, done by itself");
        }

        private void Complete(string why)
        {
            if (done) return;
            done = true;
            Debug.Log("TestAction: completed (" + why + ").", this);
            if (hint != null) hint.SetActive(false);
            OnCompleted.Invoke();
        }
    }
}
