using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    [SerializeField]private float moveSpeed;
    [SerializeField]private float jumpForce;

    [Header("Dash Info")]
    [SerializeField]private float dashDuration;
    private float dashTime;
    [SerializeField]private float dashSpeed;

    [SerializeField] private float dashCoolDown;
    private float dashCoolDownTimer;

    [Header("Attack Info")]
    private bool isAttacking;
    private int comboCounter;
    private float comboTimeWindow;
    [SerializeField]private float comboTime = 0.3f;

    private float xInput;

    private int facingDirection = 1;
    private bool facingRight = true;
    
    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask wahtIsGround;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        Movement();
        CheckInput();
        CollisionChecks();

        dashTime -= Time.deltaTime;
        dashCoolDownTimer -= Time.deltaTime;
        comboTimeWindow -= Time.deltaTime;

        FlipController();
        AnimatorControllers();
    }

    public void AttackOver()
    {
        isAttacking = false;

        comboCounter++;

            if(comboCounter > 2)
            {
                comboCounter = 0;
            }
    }

    private void CollisionChecks()
    {
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, wahtIsGround);
    }

    private void CheckInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Dash();
        }
    }

    private void StartAttackEvent()
    {
        if(!isGrounded)
        {
            return;
        }
        if (comboTimeWindow < 0)
        {
            comboCounter = 0;
        }


        isAttacking = true;
        comboTimeWindow = comboTime;
    }

    private void Dash()
    {
        if(dashCoolDownTimer < 0 && !isAttacking)
        {
            dashCoolDownTimer = dashCoolDown;
            dashTime = dashDuration;
        }        
    }

    private void Movement()
    {
        if(isAttacking)
        {
            rb.linearVelocity = new Vector2(0, 0);
        }
        else if (dashTime > 0)
        {
            rb.linearVelocity = new Vector2(facingDirection * dashSpeed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(xInput * moveSpeed, rb.linearVelocity.y);
        }        
    }

    private void Jump()
    {
        if(isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }        
    }

    private void AnimatorControllers()
    {
        bool isMoving = rb.linearVelocity.x != 0;
        //isMoving = rb.linearVelocity.x != 0;
        anim.SetFloat("yVelocity", rb.linearVelocity.y);

        anim.SetBool("isMoving", isMoving);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isDashing", dashTime > 0);
        anim.SetBool("isAttacking", isAttacking);
        anim.SetInteger("ComboCounter", comboCounter);
    }

    private void Flip()
    {
        facingDirection = facingDirection * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    private void FlipController()
    {
        if(rb.linearVelocity.x > 0 && !facingRight)
        {
            Flip();
        }
        else if(rb.linearVelocity.x < 0 && facingRight)
        {
            Flip();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
