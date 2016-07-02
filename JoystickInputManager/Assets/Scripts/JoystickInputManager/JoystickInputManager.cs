using UnityEngine;
using System.Collections;

/*
    Original Framework created by Joshua Evans (TheJoshuaEvans.com) and provided under the MIT Liscence. Feel free to expand upon
    and edit this framework as you require.
     
    Copyright (c) 2016 Joshua Evans
     
    Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
    files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
    modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software 
    is furnished to do so, subject to the following conditions:
     
    The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES 
    OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE 
    LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR 
    IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

/// <summary>
///     <para>
///         The JoystickInputManager class handles all Joystick input. It is required that the input manager is appropriately set up
///         in order for this class to function. This class does NOT neet to be attached to a game object in order to work! All
///         of its functions can be accessed statically.
///     </para>
///     <para>
///         NOTE: In order for this class to function correctly, specifically when getting axis values, the Input manager must be set
///         up correctly. An InputManager.asset file should be included with this class that can be dropped into the ProjectSettings
///         folder of your project, and will automatically set up the manager, but ALL OTHER INPUTS WILL BE DELETED.
///         
///         Inputs can be added freely after setup, as additional inputs will not interfere with this class.
///     </para>
/// </summary>
public class JoystickInputManager : MonoBehaviour {

    /// <summary>
    /// Enum representing different Joystick types supported by the Joystick input system
    /// </summary>
    public enum JoystickTypes
    {
        WIRELESS_CONTROLLER,
        DEFAULT
    }

    /// <summary>
    /// Enum with values representing either the Horizontal or Vertical axis on the different sticks available
    /// </summary>
    public enum AxisTypes
    {
        LEFT_HORIZONTAL,
        LEFT_VERTICAL,
        RIGHT_HORIZONTAL,
        RIGHT_VERTICAL,
        D_HORIZONTAL,
        D_VERTICAL
    }

    /// <summary>
    /// Enum representing the different buttons on the joystick, using the Xbox 360 controller as its base. In other words, the
    /// "A" button is the bottom face button, which is the "X" button one the Play Station Joystick.
    /// </summary>
    public enum ButtonTypes
    {
        A_BUTTON,
        B_BUTTON
    }

    /// <summary>
    /// Get's the requested axis value of the requested joystick, or 0 if the joystick is not detected
    /// </summary>
    /// <param name="joystickNumber">The number of the joystick, starting at 1, NOT 0</param>
    /// <param name="axis">The Axis being requested</param>
    /// <returns>Float value of the axis, or 0 if the Joystick can not be found</returns>
    public static float getJoystickAxis(int joystickNumber, AxisTypes axis)
    {
        string axisString = "Joystick_";
        axisString += joystickNumber + "_";

        // The even numbered (considering 0 even) axises are horizontal, odd are vertical
        if ((int)axis % 2 == 0)
        {
            axisString += "Horizontal_";
        }
        else
        {
            axisString += "Vertical_";
        }

        // There are up to 3 axis based controls on the Xbox 360 joystick. The first is the Left Stick, covering the
        // x and y axis. Then the Right stick is the 3rd and 4th axis, and the d-pad can be a 5th and 6th axis. The left
        // stick is labeled as "1", the right stick is labeled as "2" and the d-pad is labeled as "3". Calculate accordingly!
        axisString += ((int)axis / 2) + 1;

        return Input.GetAxis(axisString);
    }

    /// <summary>
    ///     <para>
    ///         Returns whether or not a given joystick button on a given joystick is pressed
    ///     </para>
    ///     <para>
    ///         Button 1 equates to the "A" button on an Xbox 360 joystick
    ///     </para>
    ///     <para>
    ///         Button 2 equates to the "B" button on an Xbox 360 joystick
    ///     </para>
    /// </summary>
    /// <param name="joystickNumber">The joystick number, with 1 being the first joystick, NOT 0</param>
    /// <param name="buttonType">The type of button</param>
    /// <returns>Whether or not the button is pressed</returns>
    public static bool getJoystickButton(int joystickNumber, ButtonTypes buttonType)
    {
        return Input.GetKey(getKeyString(joystickNumber, buttonType));
    }

    /// <summary>
    /// Whether or not the provided joystick number is connected
    /// </summary>
    /// <param name="joystickNumber">The number of the joystick to check for, with joystick 1 being the first NOT joystick 0</param>
    /// <returns>Whether or not the joystick is connected</returns>
    public static bool isJoystickConnected(int joystickNumber)
    {
        // If there are fewer joysticks connected then the requested joystick number, the requested joystick is not connected
        if (Input.GetJoystickNames().Length < joystickNumber) return false;

        // If the joystick name is an empty string, then the requested joystick isn't connected either
        if (Input.GetJoystickNames()[joystickNumber - 1] == "") return false;

        return true;
    }

    /// <summary>
    ///     <para>
    ///         Will return the number of joysticks that are currently connected
    ///     </para>
    ///     <para>
    ///         NOTE: This is not neccesarily indicitive of the joystick numbers that are connected. For example, if only Joystick
    ///         number 2 is connected, but NOT joystick number 1, then a value of "1" will be returned from this function, as only
    ///         a single joystick is connected.
    ///     </para>
    /// </summary>
    /// <returns>The number of joysticks connected</returns>
    public static int getNumberOfConnectedJoysticks()
    {
        // Get the joystick name, and number
        string[] joystickNames = Input.GetJoystickNames();
        int numberOfJoysticks = joystickNames.Length;

        // Disreguard empty joystick numbers
        for (int i = 0; i < joystickNames.Length; i++)
        {
            if (joystickNames[i] == "") numberOfJoysticks--;
        }

        return numberOfJoysticks;
    }

    /// <summary>
    /// Returns the joystick type of a given joystick
    /// </summary>
    /// <param name="joystickNumber">The Joystick number</param>
    /// <returns>The joystick type</returns>
    public static JoystickTypes getJoystickTypeForJoystickNumber(int joystickNumber)
    {
        return getJoystickType(joystickNumber);
    }

    /*
        ----------------PRIVATE HELPERS----------------

        These helpers handle the calculations that determine what buttons relate to what joysticks on which platforms.
        
        It is expected that these helpers should be expanded in order to properly support additional joystick types!

        In order to add a new joystick type, first update the public JoystickTypes enum above with the new joystick's
        name, then in the private getJoystickTypes function below, add the new joystick type to the switch statement, so that
        it can be returned. Then update the getKeyString function below to apply button numbers to your new joystick type for
        the applicable platforms

        Remember to add comments so other people (including your future self!) can figure out what is going on!
    */

    /// <summary>
    ///     <para>
    ///         Uses the joystick type and platform to generate a key string for a connected joystick. This is where
    ///         the bulk of the calculations for this class are done.
    ///     </para>
    ///     <para>
    ///         NOTE: The index for joysticks begins a 1, NOT 0
    ///     </para>
    /// </summary>
    /// <param name="joystickNumber"></param>
    /// <param name="buttonType"></param>
    /// <returns></returns>
    private static string getKeyString(int joystickNumber, ButtonTypes buttonType)
    {
        string retString = "joystick " + joystickNumber + " button ";

        // This process has two layers of checks, first we check for the platform, and then we check for the joystick type
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor:

                switch (getJoystickType(joystickNumber))
                {
                    case JoystickTypes.WIRELESS_CONTROLLER:
                        // The generic wireless joystick set up on windows
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the wireless joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the wireless joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;

                    default:
                        // The default joystick set up on windows is based off of the "WIRELESS_CONTROLLER" type
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the default joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the default joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;
                }

                break;
            case RuntimePlatform.OSXPlayer | RuntimePlatform.OSXEditor:

                switch (getJoystickType(joystickNumber))
                {
                    case JoystickTypes.WIRELESS_CONTROLLER:
                        // The generic wireless joystick set up on osx
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the wireless joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the wireless joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;

                    default:
                        // The default joystick set up on OSX is based off of the "WIRELESS_CONTROLLER" type
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the default joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the default joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;
                }

                break;
            case RuntimePlatform.LinuxPlayer:

                switch (getJoystickType(joystickNumber))
                {
                    case JoystickTypes.WIRELESS_CONTROLLER:
                        // The generic wireless joystick set up on linux
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the wireless joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the wireless joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;

                    default:
                        // The default joystick set up on Linux is based off of the "WIRELESS_CONTROLLER" type
                        switch (buttonType)
                        {
                            case ButtonTypes.A_BUTTON:
                                // For the default joystick, "1" equates to the "A" button on a 360 joystick
                                retString += "1";
                                break;
                            case ButtonTypes.B_BUTTON:
                                // For the default joystick, "2" equates to the "B" button on a 360 joystick
                                retString += "2";
                                break;
                            default:
                                throw new System.Exception("Unsupported button number " + buttonType);
                        }
                        break;
                }

                break;
            default:
                // If the player type is not supported, throw an error!
                throw new System.Exception("ControllerInput only supports Windows, OSX, and Linux Unity players!");
        }

        return retString;
    }

    /// <summary>
    /// Determines what joystick type is being used in a specific joystick slot, and then returns the apropriate string
    /// </summary>
    /// <param name="joystickNumber">The joystick number (Note that this number Starts at 1, NOT 0</param>
    /// <returns>A string denoting the joystick type</returns>
    private static JoystickTypes getJoystickType(int joystickNumber)
    {
        JoystickTypes type;

        try
        {
            switch (Input.GetJoystickNames()[joystickNumber - 1])
            {
                case "Wireless Controller":
                    type = JoystickTypes.WIRELESS_CONTROLLER;
                    break;
                default:
                    type = JoystickTypes.DEFAULT;
                    break;
            }
        }
        catch
        {
            // We got an error. We probably went out of bounds, just use the default so we don't break anything
            type = JoystickTypes.DEFAULT;
            Debug.LogWarning("Attempting to get joystick type for joystick number " + joystickNumber + " that doesn't exist!");
        }

        return type;
    }
}
