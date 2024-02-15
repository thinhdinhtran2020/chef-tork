using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startScreenText : MonoBehaviour
{
    public float levelTitleTime = 3f;
    public GameObject StartCanvas;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the entering collider belongs to the player
        if (other.CompareTag("Player"))
        {
            RemoveStartText();
        }
    }

        void RemoveStartText()
    {
        StartCanvas.SetActive(false);
    }
}
