using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRhino : Enemy
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CollisionChecks();
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);

    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
    }

    protected override void Flip()
    {
        base.Flip();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }

    protected override void OnXAxisMove()
    {
        base.OnXAxisMove();
        if (wallDetected || !groundDetected)
        {
            Debug.Log("Detected!");
            Flip();
        }
    }

    protected override void OnYAxisMove()
    {
        base.OnYAxisMove();
    }

}
