using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PCInteraction : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Computer"))
        {
            Debug.Log("I am in pcinteract");
            // Get the ComputerInteraction script attached to the computer
            ComputerInteraction computerInteraction = other.GetComponent<ComputerInteraction>();

            // Interact with the computer
            if (computerInteraction != null)
            {
                computerInteraction.Interact();
            }
        }
    }
}
