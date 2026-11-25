using UnityEngine;

namespace SaintCharbel
{
    // Put this on a button. Connect PrintName() to the button's On Click () event.
    // When the button is pressed, its name is printed in the Console.
    public class ButtonNamePrinter : MonoBehaviour
    {
        public void PrintName()
        {
            Debug.Log("Button pressed: " + gameObject.name);
        }
    }
}
