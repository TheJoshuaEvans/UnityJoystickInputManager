using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    /// Get's the requested axis value of the requested joystick, or 0 if the joystick is not detected
    /// </summary>
    /// <param name="joystickNumber">The number of the joystick. Joystick numbers start at 1, NOT 0</param>
    /// <param name="axis">The Axis being requested</param>
    /// <returns>Float value of the axis, or 0 if the Joystick can not be found</returns>
    public static float getJoystickAxis(int joystickNumber, JoystickData.AxisTypes axis)
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
    /// </summary>
    /// <param name="joystickNumber">The joystick number. Joystick numbers start at 1, NOT 0</param>
    /// <param name="buttonType">The type of button</param>
    /// <returns>Whether or not the button is pressed</returns>
    public static bool getJoystickButton(int joystickNumber, JoystickData.ButtonTypes buttonType)
    {
        return Input.GetKey(getKeyString(joystickNumber, buttonType));
    }

    /// <summary>
    ///     <para>
    ///         Returns whether or not a given joystick button on a given joystick has been pressed down this tick
    ///     </para>
    /// </summary>
    /// <param name="joystickNumber">The joystick number. Joystick numbers start at 1, NOT 0</param>
    /// <param name="buttonType">The type of button</param>
    /// <returns>Whether or not the button is pressed</returns>
    public static bool getJoystickButtonDown(int joystickNumber, JoystickData.ButtonTypes buttonType)
    {
        return Input.GetKeyDown(getKeyString(joystickNumber, buttonType));
    }

    /// <summary>
    ///     <para>
    ///         Returns whether or not a given joystick button on a given joystick has been released this tick
    ///     </para>
    /// </summary>
    /// <param name="joystickNumber">The joystick number. Joystick numbers start at 1, NOT 0</param>
    /// <param name="buttonType">The type of button</param>
    /// <returns>Whether or not the button is pressed</returns>
    public static bool getJoystickButtonUp(int joystickNumber, JoystickData.ButtonTypes buttonType)
    {
        return Input.GetKey(getKeyString(joystickNumber, buttonType));
    }

    /// <summary>
    /// Whether or not the provided joystick number is connected
    /// </summary>
    /// <param name="joystickNumber">The number of the joystick to check for. Joystick numbers start at 1, NOT 0</param>
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
    /// <returns>A string representing the joystick type</returns>
    public static string getJoystickNameForJoystickNumber(int joystickNumber)
    {
        return getJoystickName(joystickNumber);
    }

    /*
        ----------------PRIVATE HELPERS----------------

        These helpers handle the calculations that determine what buttons relate to what joysticks on which platforms.
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
    private static string getKeyString(int joystickNumber, JoystickData.ButtonTypes buttonType)
    {
        string retString = "joystick " + joystickNumber + " button ";

        // First use the platform to determine which register to search
        Dictionary<string, Dictionary<JoystickData.ButtonTypes, int>> register;
        string platformString;
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsPlayer | RuntimePlatform.WindowsEditor:
                register = JoystickData.register_windows;
                platformString = "Windows";
                break;
            case RuntimePlatform.OSXPlayer | RuntimePlatform.OSXEditor:
                register = JoystickData.register_osx;
                platformString = "OSX";
                break;
            case RuntimePlatform.LinuxPlayer:
                register = JoystickData.register_linux;
                platformString = "Linux";
                break;
            default:
                // If the player type is not supported, throw an error!
                throw new System.Exception("ControllerInput only supports Windows, OSX, and Linux Unity players!");
        }

        // Attempt to find the joystick name in the register
        string joystickName = getJoystickName(joystickNumber);
        Dictionary<JoystickData.ButtonTypes, int> buttonConfig;

        if (!register.ContainsKey(joystickName))
        {
            // The joystick name wasn't found! Log so that developers know to add the joystick (if they want)
            logOnce("No " + platformString + " register exists for joystick name \"" + joystickName + "\". Switching to default register!");
            register = JoystickData.register_default;
        }
        else if (!register[joystickName].ContainsKey(buttonType))
        {
            // The button type was not found on the platform specific register, switch to the default register
            logOnce("Button of type \"" + buttonType + "\" not found on " + platformString + " register. Switching to default");
            register = JoystickData.register_default;
        }

        if (!register.ContainsKey(joystickName))
        {
            // The default register doesn't contain an entry for this joystick name, log and go to default!
            logOnce("Joystick name \"" + joystickName + "\" not found in default register. Switching to default joystick name!");
            joystickName = "default";

            if (!register.ContainsKey(joystickName))
            {
                // If we reach this code, it means the default register doesn't have a default button configuration. This is bad!
                throw new System.Exception("There is no default button configuration for the default register! Critical error!");
            }

            // While we are here, make sure there the requested button is in the default joystick config of the default register
            if (!register[joystickName].ContainsKey(buttonType))
            {
                // The requested button doesn't exist on the default joystick configuration! This is bad!
                throw new System.Exception("Button of type \"" + buttonType + "\" does not exist on default joystick configuration! Critical Error!");
            }
        }

        buttonConfig = register[joystickName];

        retString += buttonConfig[buttonType];

        return retString;
    }

    /// <summary>
    /// Determines what joystick type is being used in a specific joystick slot, and then returns the apropriate string
    /// </summary>
    /// <param name="joystickNumber">The joystick number. Joystick numbers start at 1, NOT 0</param>
    /// <returns>A string denoting the joystick type</returns>
    private static string getJoystickName(int joystickNumber)
    {
        string[] joystickNames = Input.GetJoystickNames();

        // First do some bounds checking
        if (joystickNames.Length < joystickNumber)
        {
            // We are out of bounds! Log a warning and return "default"
            Debug.LogWarning("Attempting to get a joystick type out of bounds! Number attempted: " + joystickNumber + " | Number available: " + joystickNames.Length);
            return "default";
        }

        // Now check if a name for the controller is available
        if (joystickNames[joystickNumber - 1] == "")
        {
            // The controller isn't plugged in! Log a warning and return "default"
            Debug.LogWarning("Attempting to get joystick type for unplugged joystick number: " + joystickNumber);
            return "default";
        }

        return joystickNames[joystickNumber - 1];
    }

    /// <summary>
    /// Special dictionary used to effeciently determine if a string has previously been logged
    /// </summary>
    private static Dictionary<string, bool> loggedStrings = new Dictionary<string, bool>();

    /// <summary>
    /// Logging can be surprisingly expensive, so make sure that we only log non-warnings once!
    /// </summary>
    /// <param name="log">The message that should be logged</param>
    private static void logOnce(string log)
    {
        if (!loggedStrings.ContainsKey(log))
        {
            Debug.Log(log);
            loggedStrings.Add(log, true);
        }
    }
}
