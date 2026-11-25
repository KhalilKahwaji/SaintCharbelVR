using UnityEngine;

namespace SaintCharbel
{
    // Put this on the [VR Rig] prefab, next to SeatAligner.
    // It watches the visitor's head. When the head leaves a box around the Seat,
    // it turns on a warning (a dark panel with "Please step back").
    // When the head comes back inside the box, it turns the warning off again.
    public class PlayAreaBoundary : MonoBehaviour
    {
        [Tooltip("The CenterEyeAnchor inside the rig (the visitor's head).")]
        public Transform centerEye;

        [Tooltip("The middle of the box. Leave empty to find the object named 'Seat' in the scene.")]
        public Transform seat;

        [Tooltip("Size of the box on the floor, in metres: X = width (left-right), Y = depth (front-back).")]
        public Vector2 size = new Vector2(2f, 2f);

        [Tooltip("The dark panel with 'Please step back'. It is shown only while the head is outside the box.")]
        public GameObject warning;

        void Start()
        {
            if (seat == null)
            {
                GameObject seatObject = GameObject.Find("Seat");
                if (seatObject != null) seat = seatObject.transform;
            }

            if (seat == null || centerEye == null || warning == null)
            {
                Debug.LogWarning("PlayAreaBoundary: Seat, Center Eye or Warning is missing. The boundary is off.");
                enabled = false; // stops Update from running
                return;
            }

            warning.SetActive(false);
        }

        void Update()
        {
            bool outside = !IsInside(centerEye.position);

            // Only change it when it is different, so we don't switch it every frame.
            if (warning.activeSelf != outside)
            {
                warning.SetActive(outside);
            }
        }

        // True if this point is inside the box. Height doesn't matter.
        public bool IsInside(Vector3 worldPoint)
        {
            // Turn the point into "seen from the Seat": x = to the Seat's right, z = in front of the Seat.
            Vector3 local = seat.InverseTransformPoint(worldPoint);

            return Mathf.Abs(local.x) <= size.x / 2f
                && Mathf.Abs(local.z) <= size.y / 2f;
        }

        // Draws the box as a green rectangle in the Scene view, so scene builders
        // can see where they must NOT put anything.
        void OnDrawGizmos()
        {
            Transform middle = seat;
            if (middle == null)
            {
                GameObject seatObject = GameObject.Find("Seat");
                if (seatObject == null) return;
                middle = seatObject.transform;
            }

            Gizmos.color = Color.green;
            Gizmos.matrix = Matrix4x4.TRS(middle.position, Quaternion.Euler(0f, middle.eulerAngles.y, 0f), Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(size.x, 2.4f, size.y));
        }
    }
}
