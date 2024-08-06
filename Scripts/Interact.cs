using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public string interactName;
    public GameObject interactObj;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            interactObj.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == GameManager.Instance.player.gameObject)
        {
            interactObj.SetActive(false);
        }
    }

    public void TouchInteract()
    {
        ClosableUI.OpenUI(interactName);
    }
}
