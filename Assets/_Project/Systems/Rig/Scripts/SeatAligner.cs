using System.Collections;
using UnityEngine;

namespace SaintCharbel
{
    // Put this on the [VR Rig] prefab.
    // When the scene starts, it moves and turns the rig so the visitor's eyes
    // are at the scene's "Seat" object, looking the way the Seat faces.
    public class SeatAligner : MonoBehaviour
    {
        [Tooltip("The root of the rig (the object with OVRCameraRig). Leave empty to use this object.")]
        public Transform rig;

        [Tooltip("The CenterEyeAnchor inside the rig (the visitor's eyes).")]
        public Transform centerEye;

        [Tooltip("Where the visitor's eyes should be. Leave empty to find the object named 'Seat' in the scene.")]
        public Transform seat;

        // Start can wait: we skip 2 frames so the headset has reported where the head is.
        IEnumerator Start()
        {
            if (rig == null) rig = transform;

            if (seat == null)
            {
                GameObject seatObject = GameObject.Find("Seat");
                if (seatObject != null) seat = seatObject.transform;
            }

            if (seat == null || centerEye == null)
            {
                Debug.LogWarning("SeatAligner: no Seat in the scene, or Center Eye not set. The rig stays where it is.");
                yield break;
            }

            yield return null;
            yield return null;

            AlignToSeat();
        }

        // Public so other scripts (or a button) can call it again later.
        public void AlignToSeat()
        {
            // 1. Turn: only around the vertical (Y) axis, so the floor stays flat.
            float turn = seat.eulerAngles.y - centerEye.eulerAngles.y;
            rig.RotateAround(centerEye.position, Vector3.up, turn);

            // 2. Move: shift the whole rig so the eyes land exactly on the Seat.
            Vector3 offset = seat.position - centerEye.position;
            rig.position += offset;
        }
    }
}
