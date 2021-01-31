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
    [SerializeField] private float maxHorizontalVelocity = 10f;
    [SerializeField] private float walkAnimationSpeedMultiplier = 0.5f;
    [SerializeField] private Transform characterContainer;

    private float jumpTimer;
    private bool isCharacterDirectionRight;

    private void Awake()
    {
        jumpTimer = 0f;
        isCharacterDirectionRight = true;
        SetCharacterDirection(isCharacterDirectionRight);
    }

    private void OnEnable()
    {
        CharacterInput.OnCharacterJump += OnCharacterJump;
        CharacterInput.OnCharacterReach += OnCharacterReach;
        CharacterInput.OnCharacterStand += OnCharacterStand;
    }

    private void OnDisable()
    {
        CharacterInput.OnCharacterJump -= OnCharacterJump;
        CharacterInput.OnCharacterReach -= OnCharacterReach;
        CharacterInput.OnCharacterStand -= OnCharacterStand;
    }

    private void FixedUpdate()
    {
        if (characterInput.IsMovingRight)
        {
            if (!IsJumping())
            {
                if (!animator.GetBool("Walk"))
                {
                    animator.SetBool("Walk", true);
                    animator.SetTrigger("Stand"); // Make sure idle animation is active.
                }
                animator.speed = Mathf.Max(Mathf.Abs(rigidbody.velocity.x) * walkAnimationSpeedMultiplier, 1f);
            }

            rigidbody.AddForce(new Vector2(1f * moveSpeed * Time.fixedDeltaTime, 0f), ForceMode2D.Force);

            if (!isCharacterDirectionRight)
            {
                SetCharacterDirection(true);
                isCharacterDirectionRight = true;
            }
        }
        else if (characterInput.IsMovingLeft)
        {
            if (!IsJumping())
            {
                if (!animator.GetBool("Walk"))
                {
                    animator.SetBool("Walk", true);
                    animator.SetTrigger("Stand"); // Make sure idle animation is active.
                }
                animator.speed = Mathf.Max(Mathf.Abs(rigidbody.velocity.x) * walkAnimationSpeedMultiplier, 1f);
            }

            rigidbody.AddForce(new Vector2(-1f * moveSpeed * Time.fixedDeltaTime, 0f), ForceMode2D.Force);

            if (isCharacterDirectionRight)
            {
                SetCharacterDirection(false);
                isCharacterDirectionRight = false;
            }
        }

        if (characterInput.IsMovingLeft || characterInput.IsMovingRight)
        {
            rigidbody.velocity = new Vector2(Mathf.Clamp(rigidbody.velocity.x, -maxHorizontalVelocity, maxHorizontalVelocity), rigidbody.velocity.y);
        }
        else
        {
            if (animator.GetBool("Walk"))
            {
                animator.SetBool("Walk", false);
                animator.speed = 1f;
            }
        }
    }

    private void SetCharacterDirection(bool isRight)
    {
        characterContainer.localScale = new Vector3((isRight ? 1 : -1) * Mathf.Abs(characterContainer.localScale.x), characterContainer.localScale.y, characterContainer.localScale.z);
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
        animator.ResetTrigger("Stand");
        animator.SetTrigger("Jump");
        animator.SetBool("Walk", false);
        animator.speed = 1f;
        rigidbody.AddForce(new Vector2(0f, jumpSpeed * Time.fixedDeltaTime), ForceMode2D.Impulse);
        jumpTimer = jumpTimeLimit;
    }

    private void OnCharacterReach()
    {
        if (!IsTouchingGround())
        {
            return;
        }

        animator.ResetTrigger("Stand");
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Reach");
        animator.speed = 1f;
    }

    private void OnCharacterStand()
    {
        if (!IsTouchingGround())
        {
            return;
        }

        animator.ResetTrigger("Reach");
        animator.ResetTrigger("Jump");
        animator.SetTrigger("Stand");
        animator.speed = 1f;
    }

    private bool IsTouchingGround()
    {
        return rigidbody.IsTouchingLayers(worldLayerMask);
    }

    private bool CanJump()
    {
        return jumpTimer <= 0f;
    }

    private bool IsJumping()
    {
        return !CanJump() || !IsTouchingGround();
    }
}