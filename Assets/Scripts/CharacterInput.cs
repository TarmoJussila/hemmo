using UnityEngine;
using UnityEngine.InputSystem;

public enum CharacterState
{
    Standing = 0,
    Reaching = 1,
    Jumping = 2,
}

public class CharacterInput : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private new Rigidbody2D rigidbody;

    private CharacterState currentState;

    private void Start()
    {
        currentState = CharacterState.Standing;
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
            //transform.position += new Vector3(1f * moveSpeed * Time.deltaTime, 0f);
            rigidbody.AddForce(new Vector2(1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }
        if (keyboard.leftArrowKey.isPressed)
        {
            //transform.position += new Vector3(-1f * moveSpeed * Time.deltaTime, 0f);
            rigidbody.AddForce(new Vector2(-1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }

        if (keyboard.upArrowKey.wasPressedThisFrame)
        {
            animator.ResetTrigger("Stand");
            animator.SetTrigger("Reach");
            //transform.position += new Vector3(0f, 1f * moveSpeed * Time.deltaTime);
        }
        else if (keyboard.downArrowKey.wasPressedThisFrame)
        {
            animator.ResetTrigger("Reach");
            animator.SetTrigger("Stand");
            //transform.position += new Vector3(0f, -1f * moveSpeed * Time.deltaTime);
        }

        if (keyboard.spaceKey.wasPressedThisFrame)
        {
            rigidbody.AddForce(new Vector2(0f, jumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
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