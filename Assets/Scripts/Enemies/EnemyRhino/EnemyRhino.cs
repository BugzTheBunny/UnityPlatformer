using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRhino : Enemy
{
    [SerializeField] private LayerMask whatToIgnore;
       
    [SerializeField] float agroSpeed = 8;
    [SerializeField] float stunTime;
                     private float stunTimeTimer;

    private RaycastHit2D playerDetection;
    private bool isAggresive;

    protected override void Start()
    {
        base.Start();
        invincible = true;
    }

    protected override void Update()
    {

        playerDetection = Physics2D.Raycast(wallCheck.position, Vector2.right * facingDirection, 35, ~whatToIgnore);
        if (playerDetection.collider.GetComponent<PlayerController>())
        {
            isAggresive = true;
        }
        if (!isAggresive)
        {
            base.Update();
            rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);

            if (wallDetected || !groundDetected)
            {
                Flip();
            }
        }
        else
        {
            rb.velocity = new Vector2((agroSpeed) * facingDirection, rb.velocity.y);
            if (wallDetected && invincible)
            {
                invincible = false;
                stunTimeTimer = stunTime;
            }

            if (stunTimeTimer <= 0 && !invincible)
            {
                invincible = true;
                Flip();
                isAggresive = false;
            }
            stunTimeTimer -= Time.deltaTime;
        }

        anim.SetBool("invincible", invincible);
        AnimatorCotroller();
    }

    private void AnimatorCotroller()
    {
        CollisionChecks();
        OnXAxisMove();
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
    }

    protected override void Flip()
    {
        base.Flip();
    }

    protected override void OnXAxisMove()
    {
        anim.SetFloat("xVelocity", rb.velocity.x);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck.position, new Vector2(wallCheck.position.x + playerDetection.distance * facingDirection, wallCheck.position.y));
    }
}
