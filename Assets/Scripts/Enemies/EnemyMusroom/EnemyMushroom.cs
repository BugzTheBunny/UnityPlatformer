using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        CollisionChecks();
        OnXAxisMove();

 
    }
    protected override void OnXAxisMove()
    {
        anim.SetBool("isMoving", isMovingOnX);
        if (wallDetected || !groundDetected)
        {
            Flip();
        }
    }

    protected override void OnYAxisMove()
    {
        base.OnYAxisMove();
    }

}
