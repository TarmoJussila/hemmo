using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

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
            transform.position += new Vector3(1f * moveSpeed * Time.deltaTime, 0f);
        }
        if (keyboard.leftArrowKey.isPressed)
        {
            transform.position += new Vector3(-1f * moveSpeed * Time.deltaTime, 0f);
        }
        if (keyboard.upArrowKey.isPressed)
        {
            transform.position += new Vector3(0f, 1f * moveSpeed * Time.deltaTime);
        }
        if (keyboard.downArrowKey.isPressed)
        {
            transform.position += new Vector3(0f, -1f * moveSpeed * Time.deltaTime);
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