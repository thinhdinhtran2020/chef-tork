using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBossCholesterol : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject Entity;
    public Transform target;
    public Collider2D coll;
    public Animator anim;


    public Transform spawnPoint;

    public Slider slider;
    public Image fill;

    public GameObject player;
    public float displayDistance = 10.0f; // this value is the distance to activate the healthbar on UI

    private float startTime;
    private float timerDuration = 5f;
    public GameObject enemyPrefab;
    public GameObject bossPlatforms;
    private GameObject currentEnemy;
    private int wave = 0;

    private bool isDead = false;
    public Transform firePoint;
    public Transform firePoint2;
    public GameObject[] bulletPrefabs;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        slider.gameObject.SetActive(false);
        startTime = Time.time;
    }


    private void Update()
    {

        if (Vector3.Distance(player.transform.position, transform.position) < displayDistance)
        {
            slider.gameObject.SetActive(true);

            float elapsedTime = Time.time - startTime;


            if (currentEnemy == null || !currentEnemy.activeSelf)
            {
                if (wave < 1)
                {
                    if (elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("cholesterol_throw");
                        Invoke("Shoot", 0.83f);
                        startTime = Time.time;
                        wave++;
                        wave++;
                    }
                }
                else
                {
                    if (wave == 2 && elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("cholesterol_jump");
                        Invoke("Stomp", 1f);
                        startTime = Time.time;
                        wave++;
                    }

                    else if (wave == 3 && elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("cholesterol_jump");
                        Invoke("Stomp", 1f);
                        startTime = Time.time;
                        wave++;
                    }
                    //  || (75 <= currentHealth / maxHealth * 100)
                    else if ((wave == 4 && elapsedTime >= timerDuration))
                    {
                        anim.SetTrigger("cholesterol_throw");
                        Invoke("Shoot", 0.83f);
                        startTime = Time.time;
                        wave = 0;

                    }
                }

            }
        }
        else
        {
            slider.gameObject.SetActive(false);
        }


    }

    void ApplyForceToPlayer()
    {

        Rigidbody2D playerRigidbody = player.GetComponent<Rigidbody2D>();


        // Calculate the direction from the boss to the player
        Vector2 forceDirection = (player.transform.position - transform.position).normalized;

        // Apply the force to the player
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, -1000);

    }
    /*
    public void SpawnPlatforms()
    {
        bossPlatforms.SetActive(true);
    }

    public void DespawnPlatforms()
    {
        bossPlatforms.SetActive(false);
    }
    */
    public void SpawnEnemies()
    {
        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        currentEnemy.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        if (isDead)
            return;

      
        currentHealth -= damage;
        slider.value = currentHealth / maxHealth * 100;
        Debug.Log(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            //SpawnPlatforms();
            anim.SetTrigger("cholesterol_death");
            StartCoroutine(DieWithBuffer());
        }
        else if (currentHealth > 0)
        {
            // Play hurt animation
            anim.SetTrigger("cholesterol_hurt");
        }
    }

    private IEnumerator DieWithBuffer()
    {
        this.enabled = false;
        // Introduce a buffer time before calling Die()
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.5f);

        Die();
    }
    void Die()
    {
        Debug.Log("enemy die");
        //animator.SetBool("IsDead", true);

        slider.gameObject.SetActive(false);
        Entity.SetActive(false);
    }

    void Shoot()
    {
        //anim.SetTrigger("shoot");

        GameObject selectedBulletPrefab = bulletPrefabs[0];

        //  GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletObject = Instantiate(selectedBulletPrefab, firePoint.position, firePoint.rotation);
        bossBullet bulletScript = bulletObject.GetComponent<bossBullet>();

        bulletScript.Initialize(Vector2.left);
    }

    public void Stomp()
    {
        //  GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletObject = Instantiate(enemyPrefab, firePoint2.position, firePoint2.rotation);
        bossBullet bulletScript = bulletObject.GetComponent<bossBullet>();

        bulletScript.Initialize(Vector2.left);
    }
}
