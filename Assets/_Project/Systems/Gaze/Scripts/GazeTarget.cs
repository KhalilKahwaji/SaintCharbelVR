using UnityEngine;
using UnityEngine.Events;

namespace SaintCharbel
{
    // Put this on any object the visitor can select by looking at it.
    // The object also needs a Collider, so the GazePointer's ray can hit it.
    public class GazeTarget : MonoBehaviour
    {
        [Tooltip("What happens when the visitor has looked at this object long enough.")]
        public UnityEvent OnSelected;

        // GazePointer calls this when the ring is full.
        public void Select()
        {
            Debug.Log("Selected: " + gameObject.name);
            OnSelected.Invoke();
        }
    }
}
