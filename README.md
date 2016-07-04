# Unity Joystick Input Manager
A cross-platform joystick manager for Unity that supports multiple joystick inputs

## How to Use
There are three critical elements to this repository. There is [JoystickInputManager.cs](JoystickInputManager/Assets/Scripts/JoystickInputManager/JoystickInputManager.cs), 
[JoystickData.cs](JoystickInputManager/Assets/Scripts/JoystickInputManager/JoystickData.cs), and [InputManager.asset](JoystickInputManager/ProjectSettings/InputManager.asset).

The JoystickInputManager is a class with a series of static functions that can be called to get input data from connected joysticks. It is a framework that supports plug-and-play, and is designed to be
as easy to use as possible. This file should not need to be configured or altered, but you are free to use it as a framework for bigger and better things, if you wish.

The JoystickData is a class that holds data for different joystick configurations. It is designed to be expanded upon and improved with additional joystick configurations. Check the in-line documentation (comments)
to learn how to configure this class.

The InputManager.asset file can be be copied over the file of the same name in the ProjectSettings folder of your project. This *WILL* destroy any inputs that you have set up previously. Unfortunately, there
is no way to avoid this (that I can think of) other than manually inputting the controls yourself.
