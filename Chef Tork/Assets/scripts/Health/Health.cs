using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    public Animator anim;

    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private bool dead;
    private int Respawn;

    private void Awake()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float _damage)
    {
        if (currentHealth == 1 && !dead)
        {
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
            GetComponent<PlayerController>().enabled = false;
            GetComponent<PlayerCombat>().enabled = false;
            GetComponent<PlayerController>().Die();

            dead = true;
            StartCoroutine(DelayedRespawn());
        }
        else
        {
            anim.SetTrigger("hurt");
            currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        }

    }

    public IEnumerator DelayedRespawn()
    {
        {
            yield return new WaitForSeconds(1.0f); // Waits 1 seconds to respawn the player
            SceneManager.LoadScene(Respawn);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Orange") && currentHealth < startingHealth)
        {
            currentHealth = Mathf.Clamp(currentHealth + 1, 0, startingHealth);
            Destroy(collision.gameObject);
        }
    }
    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            TakeDamage(1);
    }*/
}
