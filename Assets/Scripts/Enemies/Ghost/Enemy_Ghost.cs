using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Ghost : Enemy
{
    [Header("Ghost Specifics")]
    [SerializeField] private float activeTime;
                     private float activeTimeTimer;


    private Transform player;
    protected override void Start()
    {
        base.Start();
        player = GameObject.Find("Player").transform;
        activeTimeTimer = activeTime;
    }

    private void Update()
    {
        activeTimeTimer -= Time.deltaTime;
        idleTimeTimer -= Time.deltaTime;

        if (activeTimeTimer > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position,player.transform.position, speed * Time.deltaTime);
        }

        if (activeTimeTimer < 0  && idleTimeTimer  < 0 && aggresive)
        {
            anim.SetTrigger("disappear");
            aggresive = false;
            idleTimeTimer = idleTime;
        }
        if (activeTimeTimer < 0 && idleTimeTimer < 0 && !aggresive)
        {
            anim.SetTrigger("appear");
            aggresive = true;
            activeTimeTimer = activeTime;

        }
    }

    public override void Damaged()
    {
        base.Damaged();
    }

    

}
