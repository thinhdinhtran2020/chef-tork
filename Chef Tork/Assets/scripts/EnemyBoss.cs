using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
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

        if(Vector3.Distance(player.transform.position, transform.position) < displayDistance)
        {
            slider.gameObject.SetActive(true);

            float elapsedTime = Time.time - startTime;

            
            if (currentEnemy == null || !currentEnemy.activeSelf)
            {
                if (wave < 2)
                {
                    if (elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("sugar_wand");
                        Invoke("SpawnEnemies", 1f);
                        startTime = Time.time;
                        wave++;
                    }
                }
                else
                {
                    if (elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("sugar_wand");
                        Invoke("SpawnPlatforms", 1f);
                        startTime = Time.time;
                        wave = 4;
                    }
                }
                if(wave == 4)
                {
                    if (elapsedTime >= timerDuration)
                    {
                        anim.SetTrigger("sugar_throw");
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

    public void SpawnPlatforms()
    {
        bossPlatforms.SetActive(true);
    }

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
            anim.SetTrigger("sugar_death");
            StartCoroutine(DieWithBuffer());
        }
        else if(currentHealth > 0)
        {
            // Play hurt animation
            anim.SetTrigger("sugar_hurt");
        }
    }

    private IEnumerator DieWithBuffer()
    {
        this.enabled = false;
        // Introduce a buffer time before calling Die()
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1.05f);

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

        GameObject selectedBulletPrefab = bulletPrefabs[Random.Range(0, bulletPrefabs.Length)];

        //  GameObject bulletObject = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletObject = Instantiate(selectedBulletPrefab, firePoint.position, firePoint.rotation);
        bossBullet bulletScript = bulletObject.GetComponent<bossBullet>();

        bulletScript.Initialize(Vector2.left);
    }

}
