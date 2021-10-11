using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{

    public Text toolText;

    public GameObject textObject;

    public Animator animator;

    private Queue<string> toolTips;

    // Start is called before the first frame update
    void Start()
    {
        toolTips = new Queue<string>();
        textObject.SetActive(true);
    }

    public void StartTooltip(ToolTip tip)
    {

        //animator.SetBool("isOpen", true);

        toolTips.Clear();

        foreach (string tool in tip.toolTips)
        {
            textObject.SetActive(true);
            toolText.text = tool;
        }

    }

    public void EndTooltip()
    {
        //animator.SetBool("isOpen", false);

        textObject.SetActive(false);

    }
}
