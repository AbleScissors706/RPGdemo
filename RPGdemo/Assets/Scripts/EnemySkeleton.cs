using UnityEngine;

public class EnemySkeleton : Entity
{
    bool isAttacking;


    [Header("Move Info")]
    [SerializeField] private float moveSpeed;

    [Header("Player Detection Info")]
    [SerializeField] private float playerCheckDistance;
    [SerializeField] private LayerMask whatIsPlayer;

    private RaycastHit2D isPlayerDetected;


    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();

        if (isPlayerDetected)
        {
            if (isPlayerDetected.distance > 1)
            {
                rb.linearVelocity = new Vector2(moveSpeed * 1.5f * facingDirection, rb.linearVelocity.y);

            Debug.Log("Player Detected!");
            isAttacking = false;
            }
            else
            {
            Debug.Log("Attack!" + isPlayerDetected.collider.gameObject.name);
            isAttacking = true;
            }            
        }

        if (!isGrounded || isWallDetected)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if(!isAttacking)
        {
            rb.linearVelocity = new Vector2(moveSpeed * facingDirection, rb.linearVelocity.y);
        }
        
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();

        isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right * facingDirection, playerCheckDistance, whatIsPlayer);
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + playerCheckDistance * facingDirection, transform.position.y));
    }
}
