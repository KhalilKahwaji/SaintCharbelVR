using SaintCharbel;
using UnityEngine;
using UnityEngine.InputSystem;

// Test only, delete later: press Space to force the raise, H to show the hint.
public class testkk_deletelater : MonoBehaviour
{
    public RaiseTrigger trigger;

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            trigger.ForceComplete();
        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            trigger.ShowHint();
        }
    }
}
