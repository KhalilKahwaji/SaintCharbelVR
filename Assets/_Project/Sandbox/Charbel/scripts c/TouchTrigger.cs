using UnityEngine;
using UnityEngine.Events;

namespace SaintCharbel
{
    // A scene action: completes when either of the visitor's hands touches this object.
    // Needs a Collider on the same object (Box, Sphere or Capsule).
    [RequireComponent(typeof(Collider))]
    public class TouchTrigger : MonoBehaviour
    {
        [Tooltip("Points on the visitor's hands, for example LeftHandAnchor and RightHandAnchor from the Camera Rig.")]
        public Transform[] handPoints;

        [Tooltip("How close (in metres) a hand point must come to the object's surface to count as a touch.")]
        public float touchDistance = 0.05f;

        [Tooltip("Optional: a small hand symbol near the object. Hidden at start, shown by ShowHint().")]
        public GameObject hint;

        [Tooltip("Called once, when the object has been touched.")]
        public UnityEvent OnCompleted;

        private Collider objectCollider;
        private bool completed;

        void Start()
        {
            objectCollider = GetComponent<Collider>();

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

            foreach (Transform point in handPoints)
            {
                if (point == null)
                {
                    continue;
                }

                // ClosestPoint gives the spot on the collider nearest to the hand.
                // If the hand is inside the collider, it returns the hand's own position (distance 0).
                Vector3 closest = objectCollider.ClosestPoint(point.position);
                if (Vector3.Distance(closest, point.position) <= touchDistance)
                {
                    Complete();
                    return;
                }
            }
        }

        // Shows the hand symbol, to tell the visitor what to do.
        [ContextMenu("Show Hint")]
        public void ShowHint()
        {
            if (completed || hint == null)
            {
                return;
            }

            hint.SetActive(true);
        }

        // The action happens by itself, as if the visitor had touched the object.
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

            Debug.Log("Touched!");
            OnCompleted.Invoke();
        }
    }
}
