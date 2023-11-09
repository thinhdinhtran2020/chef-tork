using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeDamage : MonoBehaviour
{
    [SerializeField] private float damage;
   
    private float damageCooldown = 1.0f;
    private float lastDamageTime = -1.0f;
    public int Respawn; 

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Time.time >= lastDamageTime + damageCooldown)
        {
            lastDamageTime = Time.time;
            collision.GetComponent<Health>().TakeDamage(damage);
            
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