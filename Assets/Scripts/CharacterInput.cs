using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public delegate void CharacterEvent();
    public static CharacterEvent OnCharacterJump;
    public static CharacterEvent OnCharacterReach;
    public static CharacterEvent OnCharacterSit;

    public bool IsMovingLeft { get; private set; }
    public bool IsMovingRight { get; private set; }

    private void Start()
    {
        IsMovingLeft = false;
        IsMovingRight = false;
    }

    private void Update()
    {
        UpdateKeyboardInput();
        UpdateGamepadInput();
    }

    private void UpdateKeyboardInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null)
        {
            return;
        }

        if (keyboard.rightArrowKey.isPressed)
        {
            IsMovingRight = true;
        }
        else if (keyboard.leftArrowKey.isPressed)
        {
            IsMovingLeft = true;
        }
        else
        {
            IsMovingRight = false;
            IsMovingLeft = false;
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            OnCharacterJump?.Invoke();
        }
        else if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            OnCharacterReach?.Invoke();
        }
        else if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            OnCharacterSit?.Invoke();
        }
    }

    private void UpdateGamepadInput()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            return;
        }
    }
}