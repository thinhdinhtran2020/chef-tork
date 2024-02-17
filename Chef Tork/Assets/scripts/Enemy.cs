using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class Enemy : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public GameObject Entity;
    public Transform target;
    public Collider2D coll;

    [SerializeField] FloatingHealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        Transform commonParent = transform.parent;
        healthBar = commonParent.GetComponentInChildren<FloatingHealthBar>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Debug.Log(currentHealth);
        // Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeMeleeDamage(int damage, GameObject sender)
    {
        currentHealth -= damage;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        EnemyKnockBack enemyScript = GetComponent<EnemyKnockBack>();
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }

        if (Entity.activeSelf && enemyScript != null)
        {
            GetComponent<EnemyKnockBack>().PlayFeedback(sender);
        }
        // Play hurt animation



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

