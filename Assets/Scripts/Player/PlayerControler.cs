using System.Collections;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] float WalkSpeed = 3f;
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float CrouchSpeed = 1.5f;

    float MoveSpeed;

    public enum MoveState
    {
        Walk,
        Run,
        Crouch
    }
    MoveState CurrentsMoveState = MoveState.Walk;

    Rigidbody2D rb;
    Vector2 movement;

    Animator animator;
    SpriteRenderer spriteRenderer;

    [Space]
    [Header("Sprite Settings")]
    [SerializeField] Sprite IdleSprite;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MoveInput();
        RotationInput();
        Animate();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * MoveSpeed * Time.deltaTime);
    }

    void MoveInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movement.x, movement.y).normalized;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            CurrentsMoveState = MoveState.Run;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            CurrentsMoveState = MoveState.Crouch;
        }
        else
        {
            CurrentsMoveState = MoveState.Walk;
        }

        switch (CurrentsMoveState)
        {
            case MoveState.Run:
                MoveSpeed = RunSpeed;
                break;
            case MoveState.Crouch:
                MoveSpeed = CrouchSpeed;
                break;
            default:
                MoveSpeed = WalkSpeed;
                break;
        }

    }
    void RotationInput()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }

    void Animate()
    {
        bool Moving = movement.magnitude > 0.1f;
        animator.SetBool("IsMoving", Moving);

        if (!Moving)
        {
            animator.enabled = false;
            spriteRenderer.sprite = IdleSprite;
        }
        else
        {
            animator.enabled = true;
        }
    }
}
