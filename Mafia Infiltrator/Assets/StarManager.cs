using UnityEngine;

public class StarManager : MonoBehaviour
{
    [SerializeField] private GameObject starCanvas;
    [SerializeField] private GameObject oneStarPanel;
    [SerializeField] private GameObject twoStarPanel;
    [SerializeField] private GameObject threeStarPanel;

    private bool activated = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("goToNextLevel") && !activated)
        {
            activated = true;
            DisplayStars();
        }
    }

    private void DisplayStars()
    {
        healthSystem healthSystem = FindObjectOfType<healthSystem>();
        int currentHealth = healthSystem.currentHealth;

        starCanvas.SetActive(true); // Activate the star canvas

        // Activate the corresponding star panel based on the current health
        if (currentHealth >= 3)
        {
            ActivatePanel(threeStarPanel);
        }
        else if (currentHealth == 2)
        {
            ActivatePanel(twoStarPanel);
        }
        else if (currentHealth == 1)
        {
            ActivatePanel(oneStarPanel);
        }
    }

    private void ActivatePanel(GameObject panel)
    {
        // Deactivate all star panels first
        oneStarPanel.SetActive(false);
        twoStarPanel.SetActive(false);
        threeStarPanel.SetActive(false);

        // Activate the specified panel
        panel.SetActive(true);
    }
}
