using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;

    [SerializeField] protected int facingDirection = 1;
    
    [Header("- Detectors")]
    [SerializeField] protected LayerMask whatIsGround;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected Transform wallCheck;
    protected bool wallDetected;
    protected bool groundDetected;

    [SerializeField] protected float speed;
    protected bool isMovingOnX;
    protected bool isMovingOnY;

    public bool invincible;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        FreezOnZ();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>() != null)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            player.KnockBack(transform);
        }
    }

    protected virtual void Update()
    {
        CheckAxistMovement();
    }

    public void Damaged()
    {
        if (!invincible)
        {
            anim.SetTrigger("isHitted");
        }
    }

    protected virtual void Flip()
    {
        facingDirection = facingDirection * -1;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void CollisionChecks()
    {
        groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
        wallDetected = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, wallCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector2(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + wallCheckDistance * facingDirection, wallCheck.position.y));

    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
    

    protected virtual void FreezOnZ()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void CheckAxistMovement()
    {
        isMovingOnX = rb.velocity.x != 0;
        isMovingOnY = rb.velocity.y != 0;

    }

    protected virtual void OnXAxisMove()
    {
    }

    protected virtual void OnYAxisMove()
    {
    }
}
