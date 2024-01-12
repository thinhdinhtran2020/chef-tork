using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject Entity;
    public Transform target;
    public Collider2D coll;

    public Slider slider;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }


    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        slider.value = currentHealth/maxHealth * 100;
        Debug.Log(currentHealth);
        // Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("enemy die");

        //animator.SetBool("IsDead", true);

        this.enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Entity.SetActive(false);
    }

}
