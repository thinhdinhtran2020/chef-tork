using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;
    [SerializeField] private Transform enemyTransform; // Assign this in inspector.
    [SerializeField] private Vector3 offset;
    private Quaternion constantRotation;

    private void Start()
    {
        constantRotation = transform.rotation;
    }
    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyTransform != null)
        {
            transform.position = enemyTransform.position + offset;
        }
        transform.rotation = constantRotation;
    }
}
