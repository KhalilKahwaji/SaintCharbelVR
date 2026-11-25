using SaintCharbel;
using UnityEngine;
using UnityEngine.InputSystem;

// Test only, delete later.
// Grab cube (RaiseTrigger):  H = show hint,  Space = force complete (rises by itself)
// Touch cube (TouchTrigger): J = show hint,  K = force complete (prints "Touched!")
// Tradition label:           L = show / hide
// Sound (MainMixer):         N = test narration (wind ducks),  B = bell on your left
public class testkk_deletelater : MonoBehaviour
{
    public RaiseTrigger trigger;
    public TouchTrigger touchTrigger;
    public TraditionLabel label;
    public AudioSource narration;
    public AudioSource sfx;

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

        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            Debug.Log("N pressed: play test narration (the wind should get quieter)");
            if (narration != null) narration.Play();
        }

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            Debug.Log("B pressed: bell from the SFX object (should come from your left)");
            if (sfx != null) sfx.Play();
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Debug.Log("L pressed: tradition label Toggle");
            if (label != null) label.Toggle();
            else Debug.LogWarning("Drag TraditionLabel into the Label field of testkk_deletelater.");
        }

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Debug.Log("K pressed: touch cube ForceComplete");
            if (touchTrigger != null) touchTrigger.ForceComplete();
            else Debug.LogWarning("Drag TouchCube into the Touch Trigger field of testkk_deletelater.");
        }
    }
}
