using UnityEngine;

public class Interactable : MonoBehaviour
{

    public bool hasInteracted = false;
    public bool isInDistance = false;
    public bool inTrigger = false;

    public virtual void Interact()
    {
        // This method is meant to be overwritten
    }

    public virtual void Detach()
    {
        // This method is meant to be overwritten
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Interact();
            isInDistance = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        inTrigger = false;
        if (collision.gameObject.tag == "Player")
        {
            Detach();
            isInDistance = false;
        }
    }
            

    public void OnFocused()
    {
        hasInteracted = false;
    }

    public void OnDefocused()
    {
        hasInteracted = false;
    }

}
