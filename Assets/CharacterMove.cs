using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private CharacterInput characterInput;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private LayerMask worldLayerMask;
    [SerializeField] private float jumpTimeLimit = 0.1f;

    private float jumpTimer = 0f;

    private void OnEnable()
    {
        CharacterInput.OnCharacterJump += OnCharacterJump;
        CharacterInput.OnCharacterReach += OnCharacterReach;
        CharacterInput.OnCharacterSit += OnCharacterSit;
    }

    private void OnDisable()
    {
        CharacterInput.OnCharacterJump -= OnCharacterJump;
        CharacterInput.OnCharacterReach -= OnCharacterReach;
        CharacterInput.OnCharacterSit -= OnCharacterSit;
    }

    private void FixedUpdate()
    {
        if (characterInput.IsMovingRight)
        {
            rigidbody.AddForce(new Vector2(1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }
        else if (characterInput.IsMovingLeft)
        {
            rigidbody.AddForce(new Vector2(-1f * moveSpeed * Time.deltaTime, 0f), ForceMode2D.Force);
        }
    }

    private void LateUpdate()
    {
        if (jumpTimer > 0f)
        {
            jumpTimer -= Time.deltaTime;
        }
    }

    private void OnCharacterJump()
    {
        if (!CanJump() || !IsTouchingGround())
        {
            return;
        }

        animator.ResetTrigger("Reach");
        animator.SetTrigger("Stand");
        rigidbody.AddForce(new Vector2(0f, jumpSpeed * Time.deltaTime), ForceMode2D.Impulse);
        jumpTimer = jumpTimeLimit;
    }

    private void OnCharacterReach()
    {
        if (!IsTouchingGround())
        {
            return;
        }

        animator.ResetTrigger("Stand");
        animator.SetTrigger("Reach");
    }

    private void OnCharacterSit()
    {
        if (!IsTouchingGround())
        {
            return;
        }

        animator.ResetTrigger("Reach");
        animator.SetTrigger("Stand");
    }

    private bool IsTouchingGround()
    {
        return rigidbody.IsTouchingLayers(worldLayerMask);
    }

    private bool CanJump()
    {
        return jumpTimer <= 0f;
    }
}