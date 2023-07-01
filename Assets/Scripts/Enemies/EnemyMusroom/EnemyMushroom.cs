using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMushroom : Enemy
{
    [SerializeField] private float speed;

    protected override void Start()
    {
        base.Start();
        FreezOnZ();
    }

    protected override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(speed * facingDirection, rb.velocity.y);
        CollisionChecks();
        OnXAxisMove();
        OnYAxisMove();

 
    }
    protected override void OnXAxisMove()
    {
        base.OnXAxisMove();
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
