using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Standing = 0,
    Sitting = 1,
    Reaching = 2,
    Jumping = 3,
    MovingLeft = 4,
    MovingRight = 5,
}

public enum CharacterMoveState
{
    None = 0,
    MoveLeft = 1,
    MoveRight = 2
}

public class CharacterInput : MonoBehaviour
{
    public CharacterState CurrentState { get; private set; }
    //public CharacterMoveState CurrentMoveState { get; private set; }

    private void Start()
    {
        CurrentState = CharacterState.Standing;
        //CurrentMoveState = CharacterMoveState.None;
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
            CurrentState = CharacterState.MovingRight;
            //transform.position += new Vector3(1f * moveSpeed * Time.deltaTime, 0f);
            //GetComponent<Rigidbody>().AddForce(new Vector2(1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }
        else if (keyboard.leftArrowKey.isPressed)
        {
            CurrentState = CharacterState.MovingLeft;
            //transform.position += new Vector3(-1f * moveSpeed * Time.deltaTime, 0f);
            //GetComponent<Rigidbody>().AddForce(new Vector2(-1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            CurrentState = CharacterState.Jumping;
            //GetComponent<Rigidbody>().AddForce(new Vector2(0f, jumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
            return;
        }

        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            CurrentState = CharacterState.Reaching;
            //animator.ResetTrigger("Stand");
            //animator.SetTrigger("Reach");
            //transform.position += new Vector3(0f, 1f * moveSpeed * Time.deltaTime);
        }
        else if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            CurrentState = CharacterState.Standing;
            //animator.ResetTrigger("Reach");
            //animator.SetTrigger("Stand");
            //transform.position += new Vector3(0f, -1f * moveSpeed * Time.deltaTime);
        }

        /*else if (keyboard.upArrowKey.wasReleasedThisFrame)
        {
            Debug.Log("was released!");
            animator.SetTrigger("Stand");
        }*/

        /*if (keyboard.downArrowKey.isPressed)
        {
            transform.position += new Vector3(0f, -1f * moveSpeed * Time.deltaTime);
        }*/
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