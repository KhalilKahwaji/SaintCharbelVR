using UnityEngine;

namespace SaintCharbel
{
    // Put this on CenterEyeAnchor (the VR camera).
    // Every frame it shoots an invisible line (a "ray") straight out of the
    // visitor's eyes. If the line hits a GazeTarget for long enough, that
    // target is selected. Looking away resets the timer.
    public class GazePointer : MonoBehaviour
    {
        [Tooltip("How far the visitor can select things, in metres.")]
        public float maxDistance = 10f;

        [Tooltip("How many seconds the visitor must look at a target to select it.")]
        public float selectTime = 2f;

        // The target we are looking at now (null = none).
        private GazeTarget currentTarget;

        // How long we have been looking at currentTarget, in seconds.
        private float timer;

        // True once currentTarget has been selected, so it is selected only once.
        private bool alreadySelected;

        void Update()
        {
            GazeTarget lookedAt = null;

            // The ray starts at the camera and goes the way the camera faces.
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance))
            {
                // Is the object we hit a GazeTarget? (null if it isn't)
                lookedAt = hit.collider.GetComponent<GazeTarget>();
            }

            // Looking at something new (or at nothing): start again from zero.
            if (lookedAt != currentTarget)
            {
                currentTarget = lookedAt;
                timer = 0f;
                alreadySelected = false;
            }

            // Not looking at a target: nothing more to do this frame.
            if (currentTarget == null)
            {
                return;
            }

            // Still looking at the same target: count the time.
            timer += Time.deltaTime;

            if (timer >= selectTime && !alreadySelected)
            {
                alreadySelected = true;
                currentTarget.Select();
            }
        }
    }
}
