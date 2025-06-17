using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Setting")]
    [SerializeField] float WalkSpeed = 3f;
    [SerializeField] float RunSpeed = 5f;
    [SerializeField] float CrouchSpeed = 1.5f;

    float MoveSpeed;
    public bool IsArmed = false;

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
    private SpriteRenderer spriteRenderer;

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
        WalkAnimate();

    }

    void FixedUpdate()
    {
        if (movement != Vector2.zero)
        {
            rb.MovePosition(rb.position + movement * MoveSpeed * Time.deltaTime);
        }
    }

    void MoveInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement = new Vector2(movement.x, movement.y);

        if (movement.magnitude > 0.1f)
        {
            movement = movement.normalized;
        }
        else
        {
            movement = Vector2.zero;
        }

        //previousMoveState = CurrentsMoveState;

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
                AnimateCrounch(1f);
                break;
            case MoveState.Crouch:
                MoveSpeed = CrouchSpeed;
                AnimateCrounch(0.5f);
                break;
            default:
                MoveSpeed = WalkSpeed;
                AnimateCrounch(1f);
                break;
        }

    }
    void RotationInput()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - rb.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rb.MoveRotation(angle - 90);
    }

    void WalkAnimate()
    {
        bool Moving = movement.magnitude > 0.1f;
        animator.SetBool("IsMoving", Moving);

        if (Input.GetKeyDown(KeyCode.G))
        {
            IsArmed = true;
        }
         
        if (Input.GetKeyDown(KeyCode.F))
        {
            IsArmed = false;
        }

        if (IsArmed)
        {
            animator.SetBool("IsArmed", IsArmed);
        }
        else if (!IsArmed)
        {
            animator.SetBool("IsArmed", IsArmed);
        }
    }
    void AnimateCrounch(float alpha)
    {
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = alpha;
            spriteRenderer.color = newColor;
        }
    }
}
