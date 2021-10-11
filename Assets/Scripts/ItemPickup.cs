using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPickup : Interactable
{

    public Item item;

    public Text text;

    public ToolTip toolTip;

    void PickUp()
    {
        // Add to inventory
        bool wasPickedUp = Inventory.instance.Add(item);
        
        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
    }

    public override void Interact()
    {
        FindObjectOfType<TooltipManager>().StartTooltip(toolTip);
        inTrigger = true;
    }

    public override void Detach()
    {
        FindObjectOfType<TooltipManager>().EndTooltip();
        inTrigger = false;
    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
            Detach();
        }
    }

}
