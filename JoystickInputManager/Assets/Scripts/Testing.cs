using UnityEngine;
using System.Collections;

public class Testing : MonoBehaviour
{
    // Update is called once per frame
    void Update ()
    {
        // Get the type of joystick for the first joystick
        Debug.Log(JoystickInputManager.getJoystickTypeForJoystickNumber(1));

        // Get the number of connected joysticks
        Debug.Log(JoystickInputManager.getNumberOfConnectedJoysticks());

        // Determine whether or not the A button is pressed on the first joystick
        Debug.Log(JoystickInputManager.getJoystickButton(1, JoystickInputManager.ButtonTypes.A_BUTTON));

        // Determine whether or not the B buton is pressed on the first joystick
        Debug.Log(JoystickInputManager.getJoystickButton(1, JoystickInputManager.ButtonTypes.B_BUTTON));

        // Get the horizontal axis of the left stick on the first joystick
        Debug.Log(JoystickInputManager.getJoystickAxis(1, JoystickInputManager.AxisTypes.LEFT_HORIZONTAL));

        // The the vertical axis of the left stick on the first joystick
        Debug.Log(JoystickInputManager.getJoystickAxis(1, JoystickInputManager.AxisTypes.LEFT_VERTICAL));
    }
}
