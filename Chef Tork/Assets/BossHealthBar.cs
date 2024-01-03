using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    [SerializeField] public Slider slider;

    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider.value = currentValue / maxValue;
    }
 
}
