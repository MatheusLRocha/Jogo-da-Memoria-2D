using UnityEngine.InputSystem;

class WASDInput : IPlayerInput
{
    public bool Left() => Keyboard.current.aKey.wasPressedThisFrame;
    public bool Right() => Keyboard.current.dKey.wasPressedThisFrame;
    public bool Up() => Keyboard.current.wKey.wasPressedThisFrame;
    public bool Down() => Keyboard.current.sKey.wasPressedThisFrame;
    public bool Confirm() => Keyboard.current.enterKey.wasPressedThisFrame;
}