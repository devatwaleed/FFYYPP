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
    public void CheckCombination(GameObject doorAccessButton)
{
    foreach (DoorCombination combination in combinations)
    {
        if (combination.doorAccessButton == doorAccessButton)
        {
            combination.redGate.GetComponent<Teleporter>().EnableTeleporter();
            combination.redlocked.enabled = false;
            combination.redunlocked.enabled = true;
            combination.blueGate.GetComponent<Teleporter>().EnableTeleporter();
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
            combination.blueGate.GetComponent<Teleporter>().DisableTeleporter();
            combination.bluelocked.enabled = true;
            combination.blueunlocked.enabled = false;
        }
    }
    private void GetSpriteRenderers()
{
    foreach (DoorCombination combination in combinations)
    {
        Debug.Log("Finding renderers");
        combination.redlocked = combination.redGate.transform.Find("DoorLocked").GetComponent<SpriteRenderer>();
        combination.redunlocked = combination.redGate.transform.Find("DoorOpen").GetComponent<SpriteRenderer>();
        combination.bluelocked = combination.blueGate.transform.Find("DoorLocked").GetComponent<SpriteRenderer>();
        combination.blueunlocked = combination.blueGate.transform.Find("DoorOpen").GetComponent<SpriteRenderer>();
    }
}

}
