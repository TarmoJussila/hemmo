using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    private enum PlatformContact { None, Ground, Wall }

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
    private Vector3 lastCollisionPosition;

    private void Awake()
    {
        jumpTimer = 0f;
        isCharacterDirectionRight = true;
        SetCharacterDirection(isCharacterDirectionRight);
    }

    private void OnEnable()
    {
        CharacterInput.OnCharacterJump += OnCharacterJump;
    }

    private void OnDisable()
    {
        CharacterInput.OnCharacterJump -= OnCharacterJump;
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
        if (IsJumping())
        {
            return;
        }

        var platformContact = GetCurrentPlatformContact();
        if (platformContact == PlatformContact.Ground)
        {
            animator.ResetTrigger("Climb");
            animator.SetTrigger("Jump");
        }
        else if (platformContact == PlatformContact.Wall)
        {
            animator.ResetTrigger("Jump");
            animator.SetTrigger("Climb");
        }
        else
        {
            return;
        }

        animator.SetBool("Walk", false);
        animator.speed = 1f;
        rigidbody.AddForce(new Vector2(0f, jumpSpeed * Time.fixedDeltaTime), ForceMode2D.Impulse);
        jumpTimer = jumpTimeLimit;
    }

    private PlatformContact GetCurrentPlatformContact()
    {
        bool isTouchingWorld = rigidbody.IsTouchingLayers(worldLayerMask);
        PlatformContact platformContact = PlatformContact.None;

        if (isTouchingWorld)
        {
            ContactFilter2D filter = new ContactFilter2D
            {
                useTriggers = false,
                useLayerMask = true,
                layerMask = worldLayerMask
            };

            List<ContactPoint2D> contacts = new List<ContactPoint2D>();
            int contactCount = rigidbody.GetContacts(filter, contacts);
            lastCollisionPosition = Vector3.zero;

            if (contactCount > 0 && contacts.Count > 0)
            {
                for (int i = 0; i < contacts.Count; i++)
                {
                    float x = contacts[i].point.x - characterContainer.position.x;
                    float y = contacts[i].point.y - characterContainer.position.y;

                    if (y < -0.3f)
                    {
                        platformContact = PlatformContact.Ground;
                        lastCollisionPosition = new Vector3(x, y, 0f);
                        Debug.Log("Platform contact: " + platformContact + ":" + x + "," + y);
                        break;
                    }
                    else if (Mathf.Abs(x) > 0.2f)
                    {
                        platformContact = PlatformContact.Wall;
                        lastCollisionPosition = new Vector3(x, y, 0f);
                        Debug.Log("Platform contact: " + platformContact + ":" + x + "," + y);
                        break;
                    }
                }
            }
        }

        return platformContact;
    }

    private bool CanJump()
    {
        return jumpTimer <= 0f;
    }

    private bool IsJumping()
    {
        return !CanJump() || GetCurrentPlatformContact() == PlatformContact.None;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(characterContainer.position + lastCollisionPosition, 0.2f);
    }
}