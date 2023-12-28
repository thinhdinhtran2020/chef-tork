using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float speed = 2f;

    void Update()
    {
        // Move clouds horizontally
        transform.Translate(Vector3.left * speed * Time.deltaTime);

    }
}
