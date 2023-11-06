using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AggroSwitch : MonoBehaviour
{
    public Behaviour AIPathing;

    //whenever the AggroHitbox collides with the player, enable the AIPathing script
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (AIPathing.enabled == false)
            {
                AIPathing.enabled = true;
            }
        }
    }

}
