using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public GameObject patrol_pointA;
    public GameObject patrol_pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = patrol_pointA.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currentPoint.position - transform.position;
        if(currentPoint == patrol_pointB.transform)
        {
            rb.velocity = new Vector2(speed, 0);

        }
        else
        {
            rb.velocity = new Vector2(-speed, 0);
        }

        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == patrol_pointB.transform)
        {
     
            flip();
            currentPoint = patrol_pointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == patrol_pointA.transform)
        {
            flip();
            currentPoint = patrol_pointB.transform;
        }
    }

    private void flip()
    {
        
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;

        /*
                if (rb.velocity.x >= -0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);

            }
            else if (rb.velocity.x <= 0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
         */


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(patrol_pointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(patrol_pointB.transform.position, 0.5f);
        Gizmos.DrawLine(patrol_pointA.transform.position, patrol_pointB.transform.position);
    }

  
}
