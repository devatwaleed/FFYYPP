using UnityEngine;

[System.Serializable]
public class DoorCombination
{
    public GameObject doorAccessButton;
    public GameObject redGate;
    public GameObject blueGate;

    public SpriteRenderer redlocked;
    public SpriteRenderer redunlocked;
    public SpriteRenderer bluelocked;
    public SpriteRenderer blueunlocked;
}

public class DoorManager : MonoBehaviour
{
    [SerializeField] public DoorCombination[] combinations;

    public BoxCollider2D detectionCollider;

    // Use this method to check the combination and perform actions accordingly
    public void ChangeSprite(GameObject doorAccessButton)
{
    foreach (DoorCombination combination in combinations)
    {
        if (combination.doorAccessButton == doorAccessButton)
        {
            Debug.Log("Combination found");
            combination.redlocked.enabled = false;
            combination.redunlocked.enabled = true;
            combination.bluelocked.enabled = false;
            combination.blueunlocked.enabled = true;
            break;
        }
    }
}


    private void Start()
    {
        GetSpriteRenderers();
        // Disable all teleporters by default
        DisableAllTeleporters();
    }

    private void DisableAllTeleporters()
    {
        foreach (DoorCombination combination in combinations)
        {
            Debug.Log("Disabling all teleporters");
            combination.redGate.GetComponent<Teleporter>().DisableTeleporter();
            combination.redlocked.enabled = true;
            combination.redunlocked.enabled = false;
            if(combination.redlocked.enabled == true && combination.redunlocked.enabled == false)
            Debug.Log("Red gate sprite changed");
            combination.blueGate.GetComponent<Teleporter>().DisableTeleporter();
            combination.bluelocked.enabled = true;
            combination.blueunlocked.enabled = false;
            if(combination.bluelocked.enabled == true && combination.blueunlocked.enabled == false)
            Debug.Log("blue gate sprite changed");
        }
    }
    private void GetSpriteRenderers()
{
    foreach (DoorCombination combination in combinations)
    {
        Debug.Log("Finding renderers");
        combination.redlocked = combination.redGate.transform.Find("DoorLocked").GetComponent<SpriteRenderer>();
        if (combination.redlocked != null)
        {
            Debug.Log("DoorLocked sprite renderer found.");
        }
        else
        {
            Debug.Log("DoorLocked sprite renderer not found.");
        }
        
        combination.redunlocked = combination.redGate.transform.Find("DoorOpen").GetComponent<SpriteRenderer>();
        if (combination.redunlocked != null)
        {
            Debug.Log("DoorOpen sprite renderer found.");
        }
        else
        {
            Debug.Log("DoorOpen sprite renderer not found.");
        }
        
        combination.bluelocked = combination.blueGate.transform.Find("DoorLocked").GetComponent<SpriteRenderer>();
        if (combination.bluelocked != null)
        {
            Debug.Log("DoorLocked sprite renderer found.");
        }
        else
        {
            Debug.Log("DoorLocked sprite renderer not found.");
        }
        
        combination.blueunlocked = combination.blueGate.transform.Find("DoorOpen").GetComponent<SpriteRenderer>();
        if (combination.blueunlocked != null)
        {
            Debug.Log("DoorOpen sprite renderer found.");
        }
        else
        {
            Debug.Log("DoorOpen sprite renderer not found.");
        }
    }
}


}
