using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeAggro : MonoBehaviour
{
    public Behaviour AIPathing;

    //whenever the AggroHitbox exits the player, disable the AIPathing script

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (AIPathing.enabled == true)
            {
                AIPathing.enabled = false;
            }
        }
    }
}
