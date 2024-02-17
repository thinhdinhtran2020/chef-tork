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
    public float fireRate = 60f;
    float nextMeleeTime = 0f;
    float nextShootTime = 0f;
    public GameObject Player;

    public Transform firePoint;
 //   public GameObject bulletPrefab;
    public GameObject[] bulletPrefabs;

    private PlayerController playerController;
    private int comboMeleeCount = 0; //keeps track of player spamming melee button
    private float meleeInputCooldown = 0.2f;
    private float lastMeleeInputTime = 0f;
    private float lastFireInputTime = 0f;
    public float knockbackDirectionX = 1f;

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
                if (Time.time - lastMeleeInputTime > meleeInputCooldown)
                {
                    // Reset the consecutive count if there was a delay in button presses
                    comboMeleeCount = 1;
                }
                else
                {
                    comboMeleeCount++;
                }

                lastMeleeInputTime = Time.time;

                // Play the corresponding melee animation based on the consecutive count
                switch (comboMeleeCount)
                {
                    case 1:
                        Melee("Melee");
                        break;
                    case 2:
                        Melee("Melee2");
                        break;
                    case 3:
                        Melee("Melee3");
                        break;
                }

                nextMeleeTime = Time.time + 1f / meleeRate;
            }
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Time.time >= nextShootTime)
            {
                if (Input.GetKeyDown(KeyCode.X))
                {
                    Shoot();
                    lastFireInputTime = Time.time;
                    nextShootTime = Time.time + 1f / fireRate;
                }
            }
        }
    }

    
    void Melee(string animationTrigger)
    {
        anim.SetTrigger(animationTrigger);
        Vector2 knockbackDirection = new Vector2(knockbackDirectionX, 0f).normalized;
        //for attack animation^

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleePoint.position, meleeRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit Enemy: " + enemy.gameObject.name);

            // Check if the enemy is an instance of EnemyBoss
            if (enemy.GetComponent<EnemyBoss>() != null)
            {
                enemy.GetComponent<EnemyBoss>().TakeDamage(attackDamage);
                Debug.Log("ENEMYBOSS NOT NULL");
            }
            else if(enemy.GetComponent<Enemy>() != null)
            {
                enemy.GetComponent<Enemy>().TakeMeleeDamage(attackDamage, Player);
            }
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
