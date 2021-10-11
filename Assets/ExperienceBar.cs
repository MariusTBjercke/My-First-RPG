using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    PlayerStats stats;

    private void Start()
    {
        stats = PlayerManager.instance.GetComponent<PlayerStats>();
    }

    public void SetMaxExperience(int maxExperience)
    {
        slider.maxValue = maxExperience;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetExperience(int experience)
    {
        StartCoroutine(FillExperienceBar(experience));
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    IEnumerator FillExperienceBar(int experience)
    {
        // Loop for experience bar fill animation
        for (int x = 0; x < experience; x++)
        {
            slider.value += 1;
            yield return new WaitForSeconds(.01f);
        }
    }

}
