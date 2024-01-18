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

            if (apples < 10)
            {
                applesCount.text = "00" + apples;
            }
            else if (apples < 100)
            {
                applesCount.text = "0" + apples;
            }
            else if (apples < 1000)
            {
                applesCount.text = "" + apples;
            }
        }
    }

}
