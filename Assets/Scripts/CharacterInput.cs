using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    public delegate void CharacterEvent();
    public static CharacterEvent OnCharacterJump;
    public static CharacterEvent OnCharacterReach;
    public static CharacterEvent OnCharacterStand;

    public bool IsMovingLeft { get; private set; }
    public bool IsMovingRight { get; private set; }

    private StateManager stateManager;

    private void Start()
    {
        stateManager = FindObjectOfType<StateManager>();

        IsMovingLeft = false;
        IsMovingRight = false;
    }

    private void Update()
    {
        if (stateManager.CurrentState != StateType.Game)
        {
            if (IsMovingLeft || IsMovingRight)
            {
                IsMovingRight = false;
                IsMovingLeft = false;
            }
            return;
        }

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

        if (keyboard.rightArrowKey.isPressed && keyboard.leftArrowKey.isPressed)
        {
            IsMovingRight = false;
            IsMovingLeft = false;
        }
        else if (keyboard.rightArrowKey.isPressed)
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
            OnCharacterStand?.Invoke();
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