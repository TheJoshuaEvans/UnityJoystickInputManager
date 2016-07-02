# Unity Joystick Input Manager
A cross-platform joystick manager for Unity that supports multiple joystick inputs

## How to Use
There are two critical elements to this repository. The first is the [JoystickInputManager.cs](JoystickInputManager/Assets/Scripts/JoystickInputManager/JoystickInputManager.cs) file. Second is the
[InputManager.asset](JoystickInputManager/ProjectSettings/InputManager.asset) file.

The JoystickInputManager is a class with a series of static functions that can be called to get input data from connected joysticks. It is a framework that supports plug-and-play, and is designed to be
as easy to use as possible. This class can (and should) be extended to support additional controllers, buttons, and platforms. However, due to limitations in Unity, in order to get joystick axis data the
Input manager must be set up correctly.

The InputManager.asset file can be be copied over the file of the same name in the ProjectSettings folder of your project. This *WILL* destroy any inputs that you have set up previously. Unfortunately, there
is no way to avoid this (that I can think of) other than manually inputting the controls yourself.
