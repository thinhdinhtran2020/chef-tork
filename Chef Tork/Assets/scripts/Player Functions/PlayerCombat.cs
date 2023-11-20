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

    public Transform firePoint;
 //   public GameObject bulletPrefab;
    public GameObject[] bulletPrefabs;

    private PlayerController playerController;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

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

        if (Input.GetKeyDown(KeyCode.X))
        {
            Shoot();
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

    void Shoot()
    {
        anim.SetTrigger("shoot");

        GameObject selectedBulletPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];

        //  GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletObject = Instantiate(selectedBulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bulletScript = bulletObject.GetComponent<Bullet>();

        Vector2 shootDirection;
        if(playerController.IsFacingRight)
        {
            shootDirection = Vector2.right;
        }
        else
        {
            shootDirection = Vector2.left;
        }
        bulletScript.Initialize(shootDirection);
    }

    private void OnDrawGizmosSelected()
    {
        if (meleePoint == null)
            return;

        Gizmos.DrawWireSphere(meleePoint.position, meleeRange);

    }


}
