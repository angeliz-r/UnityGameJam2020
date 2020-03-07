using System;
using UnityEngine;

public enum PlayerControllerNumber {
    PLAYER_1 = 1,
    PLAYER_2 = 2
}

public enum ControlCode {
    Square, X, Circle, Triangle,
    L1, R1, L2, R2,
    Share, Option, L3, R3, PadPress,
    LeftStickX, LeftStickY,
    RightStickX, RightStickY,
    DpadX, DpadY
}

/// <summary>
/// This Script should be put as a component of the player.
/// Set the Joystick number on the Inspector.
/// You can then call the GetButton, GetButtonDown and etc in one of your player scripts to retrieve the button press.
/// This works similarly to Unity's Input Manager.
/// </summary>
public class DualShock4Input : MonoBehaviour
{
    [Tooltip("Sets the Joystick Number. \n(ex. PLAYER_1 for Joystick 1)")]
    [SerializeField]
    private PlayerControllerNumber _joystickPlayerNum;

    public event Action EventSquarePress = () => { };
    public event Action EventXPress = () => { };
    public event Action EventCirclePress = () => { };
    public event Action EventTrianglePress = () => { };
    public event Action EventL1Press = () => { };
    public event Action EventR1Press = () => { };
    public event Action EventL2Press = () => { };
    public event Action EventR2Press = () => { };
    public event Action EventSharePress = () => { };
    public event Action EventOptionsPress = () => { };
    public event Action EventL3Press = () => { };
    public event Action EventR3Press = () => { };
    public event Action EventPSPress = () => { };
    public event Action EventPadPress = () => { };
    public event Action EventLeftStickXPress = () => { };
    public event Action EventLeftStickYPress = () => { };
    public event Action EventRightStickXPress = () => { };
    public event Action EventRightStickYPress = () => { };
    public event Action EventDpadXPress = () => { };
    public event Action EventDpadYPress = () => { };

    /// <summary>
    /// Gets the bool value of the PS4 Button when HELD
    /// </summary>
    /// <param name="code">ex. ControlCode.Square</param>
    /// <returns></returns>
    public bool GetButton(ControlCode code) {     
        return Input.GetButton(code + "_" + (int)_joystickPlayerNum);    
    }

    /// <summary>
    /// Gets the bool value of the PS4 Button at press DOWN
    /// </summary>
    /// <param name="code">ex. ControlCode.Square</param>
    /// <returns></returns>
    public bool GetButtonDown(ControlCode code) {
        CallEvent(code);
        return Input.GetButtonDown(code + "_" + (int)_joystickPlayerNum);
    }

    /// <summary>
    /// Gets the bool value of the PS4 Button at button RELEASE
    /// </summary>
    /// <param name="code">ex. ControlCode.Square</param>
    /// <returns></returns>
    public bool GetButtonUp(ControlCode code) {
        return Input.GetButtonUp(code + "_" + (int)_joystickPlayerNum);
    }

    /// <summary>
    /// Gets the float raw axis value of the PS4 Analog Sticks when tilted.
    /// </summary>
    /// <param name="code">ex. ControlCode.Square</param>
    /// <returns></returns>
    public float GetAxisRaw(ControlCode code) {
        return Input.GetAxisRaw(code + "_" + (int)_joystickPlayerNum);
    }

    private void CallEvent(ControlCode code) {
        switch (code) {
            case ControlCode.Circle:
                EventCirclePress();
                break;
            case ControlCode.L1:
                EventL1Press();
                break;
            case ControlCode.L2:
                EventL2Press();
                break;
            case ControlCode.Option:
                EventOptionsPress();
                break;
            case ControlCode.PadPress:
                EventPadPress();
                break;
            case ControlCode.R1:
                EventR1Press();
                break;
            case ControlCode.R2:
                EventR2Press();
                break;
            case ControlCode.Share:
                EventSharePress();
                break;
            case ControlCode.Square:
                EventSquarePress();
                break;
            case ControlCode.Triangle:
                EventTrianglePress();
                break;
            case ControlCode.X:
                EventXPress();
                break;
            default:
                break;        
        }
    }


    #region TEST_METHODS
    void CountJoyStickNames() {
        for (int i = 0; i < Input.GetJoystickNames().Length; i++) {
            Debug.LogWarning(Input.GetJoystickNames()[i]);
        }
    }

    void DebugControls() {
        Debug.Log("Square " + (int)_joystickPlayerNum + " = " + Input.GetButton("Square" + "_" + (int)_joystickPlayerNum));
        Debug.Log("X " + (int)_joystickPlayerNum + " = " + Input.GetButton("X"+"_" + (int)_joystickPlayerNum));
        Debug.Log("Circle " + (int)_joystickPlayerNum + " = " + Input.GetButton("Circle" + "_" + (int)_joystickPlayerNum));
        Debug.Log("Triangle " + (int)_joystickPlayerNum + " = " + Input.GetButton("Triangle" + "_" + (int)_joystickPlayerNum));
        Debug.Log("L1 " + (int)_joystickPlayerNum + " = " + Input.GetButton("L1" + "_" + (int)_joystickPlayerNum));
        Debug.Log("R1 " + (int)_joystickPlayerNum + " = " + Input.GetButton("R1" + "_" + (int)_joystickPlayerNum));
        Debug.Log("L2 " + (int)_joystickPlayerNum + " = " + Input.GetButton("L2" + "_" + (int)_joystickPlayerNum));
        Debug.Log("R2 " + (int)_joystickPlayerNum + " = " + Input.GetButton("R2" + "_" + (int)_joystickPlayerNum));
        Debug.Log("Share " + (int)_joystickPlayerNum + " = " + Input.GetButton("Share" + "_" + (int)_joystickPlayerNum));
        Debug.Log("Options " + (int)_joystickPlayerNum + " = " + Input.GetButton("Options" + "_" + (int)_joystickPlayerNum));
        Debug.Log("L3 " + (int)_joystickPlayerNum + " = " + Input.GetButton("L3" + "_" + (int)_joystickPlayerNum));
        Debug.Log("R3 " + (int)_joystickPlayerNum + " = " + Input.GetButton("R3" + "_" + (int)_joystickPlayerNum));
        Debug.Log("PS " + (int)_joystickPlayerNum + " = " + Input.GetButton("PS" + "_" + (int)_joystickPlayerNum));
        Debug.Log("PadPress " + (int)_joystickPlayerNum + " = " + Input.GetButton("PadPress" + "_" + (int)_joystickPlayerNum));
        Debug.Log("LeftStickX " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("LeftStickX" + "_" + (int)_joystickPlayerNum));
        Debug.Log("LeftStickY " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("LeftStickY" + "_" + (int)_joystickPlayerNum));
        Debug.Log("RightStickX " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("RightStickX" + "_" + (int)_joystickPlayerNum));
        Debug.Log("RightStickY " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("RightStickY" + "_" + (int)_joystickPlayerNum));
        Debug.Log("DpadX " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("DpadX" + "_" + (int)_joystickPlayerNum));
        Debug.Log("DpadY " + (int)_joystickPlayerNum + " = " + Input.GetAxisRaw("DpadY" + "_" + (int)_joystickPlayerNum));
    }
    #endregion

}
