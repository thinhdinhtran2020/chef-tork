using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppleCollector : MonoBehaviour
{
    private int apples = 0;

    [SerializeField] private Text applesCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Apple"))
        {
            Destroy(collision.gameObject);
            apples++;
            applesCount.text = "Apples: " + apples;
        }
    }

}
