using UnityEngine;

[System.Serializable]
public class DoorCombination
{
    public GameObject doorAccessButton;
    public GameObject redGate;
    public GameObject blueGate;
}

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    public DoorCombination[] combinations;

    public BoxCollider2D detectionCollider;


    // Use this method to check the combination and perform actions accordingly
    public void CheckCombination(GameObject doorAccessButton)
    {
        foreach (DoorCombination combination in combinations)
        {
            if (combination.doorAccessButton == doorAccessButton)
            {
                // Combination found, perform actions
                combination.redGate.GetComponent<Teleporter>().EnableTeleporter();
                combination.blueGate.GetComponent<Teleporter>().EnableTeleporter();
                break;
            }
        }
    }

    private void Start()
    {
        // Disable all teleporters by default
        DisableAllTeleporters();
    }

    private void DisableAllTeleporters()
    {
        foreach (DoorCombination combination in combinations)
        {
            combination.redGate.GetComponent<Teleporter>().DisableTeleporter();
            combination.blueGate.GetComponent<Teleporter>().DisableTeleporter();
        }
    }
}
