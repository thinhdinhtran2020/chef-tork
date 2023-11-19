using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage = 5;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;

        /*
        if (moveInput.x < 0)
        {
            // If facing left, flip the object to the left
            rb.velocity = -transform.right * speed;
        }
        else
        {
            // If facing right, move the object to the right
            rb.velocity = transform.right * speed;
        }
        */
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // check to see what is being hit
        Debug.Log(collision.name);

        Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
