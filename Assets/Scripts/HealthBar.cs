using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health, int maxHealth)
    {
        StartCoroutine(DecreaseHealthBar(health, maxHealth));
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    IEnumerator DecreaseHealthBar(int health, int maxHealth)
    {
        // Loop for health bar fill animation
        for (int i = 0; i < maxHealth; i++)
        {
            float oldHealth = slider.value;
            if (oldHealth > health)
            {
                slider.value -= 1;
                yield return new WaitForSeconds(.01f);
            }
        }
    }

}
