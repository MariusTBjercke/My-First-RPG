using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupText : MonoBehaviour
{

    public Text textElement;

    public float delay = 10f;

    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        Quest.experienceAdded += ExperienceAddedText;
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        Invoke("WelcomeText", 4f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void ExperienceAddedText(int XP)
    {
        textElement.text = "You received " + XP + " experience!";
        StartCoroutine(DisableText(delay));
    }

    void WelcomeText()
    {
        textElement.enabled = true;
        textElement.text = "Press I to open your inventory.";
        StartCoroutine(DisableText(delay));
    }

    IEnumerator DisableText(float delay)
    {
        yield return new WaitForSeconds(delay);

        textElement.text = "";
    }

}
