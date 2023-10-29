using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{

    public Animator anim;
    //for attack animation^

    public Transform meleePoint;
    public float meleeRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 40;

    public float meleeRate = 60f;
    float nextMeleeTime = 0f;


    // Update is called once per frame
    void Update()
    {
        if (Time.time >= nextMeleeTime)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Melee();
                nextMeleeTime = Time.time + 1f / meleeRate;
            }
        }
    }

    void Melee()
    {
        anim.SetTrigger("Melee");
        //for attack animation^

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayers);
        
        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint == null)
            return;

        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);

    }


}
