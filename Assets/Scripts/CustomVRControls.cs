using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomVRControls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // returns true if the primary button (typically “A”) is currently pressed.
        OVRInput.Get(OVRInput.Button.One);

        // returns true if the primary button (typically “A”) was pressed this frame.
        OVRInput.GetDown(OVRInput.Button.One);

        // returns true if the “X” button was released this frame.
        OVRInput.GetUp(OVRInput.RawButton.X);

        // returns a Vector2 of the primary (typically the Left) thumbstick’s current state.
        // (X/Y range of -1.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

        // returns true if the primary thumbstick is currently pressed (clicked as a button)
        OVRInput.Get(OVRInput.Button.PrimaryThumbstick);

        // returns true if the primary thumbstick has been moved upwards more than halfway.
        // (Up/Down/Left/Right - Interpret the thumbstick as a D-pad).
        OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp);

        // returns a float of the secondary (typically the Right) index finger trigger’s current state.
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);

        // returns a float of the left index finger trigger’s current state.
        // (range of 0.0f to 1.0f)
        OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger);

        // returns true if the left index finger trigger has been pressed more than halfway.
        // (Interpret the trigger as a button).
        OVRInput.Get(OVRInput.RawButton.LIndexTrigger);

        // returns true if the secondary gamepad button, typically “B”, is currently touched by the user.
        OVRInput.Get(OVRInput.Touch.Two);
    }
}
