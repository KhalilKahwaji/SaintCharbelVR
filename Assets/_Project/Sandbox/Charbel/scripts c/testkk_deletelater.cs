using SaintCharbel;
using UnityEngine;
using UnityEngine.InputSystem;

// Test only, delete later.
// Grab cube (RaiseTrigger):  H = show hint,  Space = force complete (rises by itself)
// Touch cube (TouchTrigger): J = show hint,  K = force complete (prints "Touched!")
public class testkk_deletelater : MonoBehaviour
{
    public RaiseTrigger trigger;
    public TouchTrigger touchTrigger;

    void Update()
    {
        if (Keyboard.current == null)
        {
            return;
        }

        if (Keyboard.current.hKey.wasPressedThisFrame)
        {
            Debug.Log("H pressed: grab cube ShowHint");
            if (trigger != null) trigger.ShowHint();
            else Debug.LogWarning("Drag the grab cube into the Trigger field of testkk_deletelater.");
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space pressed: grab cube ForceComplete");
            if (trigger != null) trigger.ForceComplete();
            else Debug.LogWarning("Drag the grab cube into the Trigger field of testkk_deletelater.");
        }

        if (Keyboard.current.jKey.wasPressedThisFrame)
        {
            Debug.Log("J pressed: touch cube ShowHint");
            if (touchTrigger != null) touchTrigger.ShowHint();
            else Debug.LogWarning("Drag TouchCube into the Touch Trigger field of testkk_deletelater.");
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log("K pressed: touch cube ForceComplete");
            if (touchTrigger != null) touchTrigger.ForceComplete();
            else Debug.LogWarning("Drag TouchCube into the Touch Trigger field of testkk_deletelater.");
        }
    }
}
