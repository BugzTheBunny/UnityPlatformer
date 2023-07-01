using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            print("Entered");
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player.transform.position.x > transform.position.x)
            {
                player.KnockBack(1);
            } else if (player.transform.position.x < transform.position.x){
                player.KnockBack(-1);

            } else
            {
                player.KnockBack(0);
            }

        }
    }
}
