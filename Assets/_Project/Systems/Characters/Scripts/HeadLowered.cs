using UnityEngine;

namespace SaintCharbel
{
    // Keeps the character's head gently lowered (Charbel walks and prays with his eyes lowered).
    // Works on top of any animation, including Timeline: it runs after the animation each frame.
    [RequireComponent(typeof(Animator))]
    public class HeadLowered : MonoBehaviour
    {
        [Tooltip("How many degrees the head tilts down. 10 to 20 looks calm and natural.")]
        [Range(0f, 40f)]
        public float angle = 15f;

        private Transform head;

        void Start()
        {
            // A Humanoid Animator knows which bone is the head, whatever it is called in the model.
            head = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.Head);
        }

        // LateUpdate runs after the Animator has posed the body for this frame,
        // so we add our small tilt on top instead of being overwritten by the animation.
        void LateUpdate()
        {
            if (head == null)
            {
                return;
            }

            // Turn the head around the character's own right-hand axis: a positive angle looks down.
            head.rotation = Quaternion.AngleAxis(angle, transform.right) * head.rotation;
        }
    }
}
