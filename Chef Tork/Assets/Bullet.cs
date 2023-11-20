using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float speed = 20f;
    public int damage;
    public Rigidbody2D rb;
    public SpriteRenderer spriteRenderer;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction * speed;

        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
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
