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
/// This static class contains data relating to different controller button mappings, check the in-line documentation for how to configure
/// this data!
/// </summary>
public static class JoystickData
{
    /*
        ------------------------- HOW TO CONFIGURE THIS DATA -------------------------
        
        In order to add or alter joystick configuration, you only need to touch this class. It shouldn't
        be required to alter the "JoystickInputManager" itself unless you want to get much deeper into configuration,
        or something is broken somehow.

        ---------- HOW TO ADD JOYSTICK SUPPORT ----------

        There are 2 steps to adding a new joystick type, first you must create a public dictionary object configured to use the
        ButtonTypes enum as a key, and the int data type for values (Use the "joystick_default" dictionary below as a guide!).

        Note: This dictionary must be placed ABOVE the registers in the file.

        Once the dictionary is created, it must be added to the appropriate register. There are 4 registers, one for Windows, OSX, and
        Linux, and then a "default" register. The platform specific registers should only be used for joysticks that have different configurations
        on different platforms. If a joystick name is not found on a platform specific register, the default register will be used.

        Note: The joystick name used to register the joystick configuration much match what Unity reads as the joystick name EXACTLY. Use
        the built in helper function JoystickInputManager.getJoystickNameForJoystickNumber function to get the exact name of a joystick.

        ---------- TIPS ----------

        Only buttons that are configured differently between platforms need to be added to a platform specific configuration. If a
        specific button is not found then the default register will be used.

        ---------- WARNINGS ----------
        
        Due to limitations in the Unity Input Manager, the axis inputs are not easily configurable. Of course, you can always dive into the
        JoystickInputManager class and configure them manually. However, with out additional work, changes to the Axis Types may break the
        JoystickInputManager.

        If you add a new ButtonType then you SHOULD also add aconfiguration value to the "joystick_default" dictionary. If the button configuration
        is not found in that dictionary then an error will be thrown and the JoystickInputManager may stop working.
    */


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
    /// The default joystick configuration for the default register
    /// </summary>
    public static Dictionary<ButtonTypes, int> joystick_default = new Dictionary<ButtonTypes, int>()
    {
        { ButtonTypes.A_BUTTON, 1 },
        { ButtonTypes.B_BUTTON, 2 }
    };

    /// <summary>
    /// The register for the "default" platform. If a joystick name or button type cannot be found in a platform specific register, this register
    /// will be used instead.
    /// </summary>
    public static Dictionary<string, Dictionary<ButtonTypes, int>> register_default = new Dictionary<string, Dictionary<ButtonTypes, int>>()
    {
        { "default", JoystickData.joystick_default }
    };

    /// <summary>
    /// The register used on Windows platforms.
    /// </summary>
    public static Dictionary<string, Dictionary<ButtonTypes, int>> register_windows = new Dictionary<string, Dictionary<ButtonTypes, int>>()
    {
        
    };

    /// <summary>
    /// The register used on OSX platforms
    /// </summary>
    public static Dictionary<string, Dictionary<ButtonTypes, int>> register_osx = new Dictionary<string, Dictionary<ButtonTypes, int>>()
    {
        
    };

    /// <summary>
    ///  The register used on Linux platforms
    /// </summary>
    public static Dictionary<string, Dictionary<ButtonTypes, int>> register_linux = new Dictionary<string, Dictionary<ButtonTypes, int>>()
    {

    };
}
