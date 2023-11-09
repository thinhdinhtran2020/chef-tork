using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private float damage;
    public int Respawn;
    private float damageCooldown = 2.0f;
    private float lastDamageTime = -2.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Time.time >= lastDamageTime + damageCooldown)
        {
            lastDamageTime = Time.time;
            collision.GetComponent<Health>().TakeDamage(damage);
            //StartCoroutine(DelayedRespawn()); do a check later to only execute this if health == 0
        }
    }
    public IEnumerator DelayedRespawn()
    {
        {
            yield return new WaitForSeconds(1.0f); // Waits 1 seconds to respawn the player
            SceneManager.LoadScene(Respawn);
        }
    }
}