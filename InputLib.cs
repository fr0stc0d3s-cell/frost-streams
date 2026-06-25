using Valve.VR;

namespace frost.Libraries;

internal class InputLib
{
    public static bool RightGrab => ControllerInputPoller.instance.rightControllerGripFloat > 0.5f;
    public static bool LeftGrab => ControllerInputPoller.instance.leftControllerGripFloat > 0.5f;
    public static bool RightTrigger => ControllerInputPoller.instance.rightControllerTriggerButton;
    public static bool LeftTrigger => ControllerInputPoller.instance.leftControllerTriggerButton;

    // A
    public static bool A => ControllerInputPoller.instance.rightControllerPrimaryButton;
    // B 
    public static bool B => ControllerInputPoller.instance.rightControllerSecondaryButton;
    // Y 
    public static bool Y => ControllerInputPoller.instance.leftControllerPrimaryButton;
    // X
    public static bool X => ControllerInputPoller.instance.leftControllerSecondaryButton;

    public static bool RightJoystickClick
    {
        get
        {
            if (SteamVR_Actions.gorillaTag_RightJoystickClick == null)
                return false;

            return SteamVR_Actions.gorillaTag_RightJoystickClick.state;
        }
    }

    public static bool LeftJoystickClick
    {
        get
        {
            if (SteamVR_Actions.gorillaTag_LeftJoystickClick == null)
                return false;

            return SteamVR_Actions.gorillaTag_LeftJoystickClick.state;
        }
    }
}