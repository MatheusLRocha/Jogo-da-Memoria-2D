using UnityEngine.InputSystem;

class ArrowInput : IPlayerInput
{
    public bool Left() => Keyboard.current.leftArrowKey.wasPressedThisFrame;
    public bool Right() => Keyboard.current.rightArrowKey.wasPressedThisFrame;
    public bool Up() => Keyboard.current.upArrowKey.wasPressedThisFrame;
    public bool Down() => Keyboard.current.downArrowKey.wasPressedThisFrame;
    public bool Confirm() => Keyboard.current.enterKey.wasPressedThisFrame;
}