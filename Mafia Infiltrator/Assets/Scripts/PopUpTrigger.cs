using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using System.Collections;

[System.Serializable]
public class QuizQuestion
{
    public string question;
    public string[] options;
    public string answer;
}

[System.Serializable]
public class QuizData
{
    public QuizQuestion[] questions;
}

public class PopUpTrigger : MonoBehaviour
{
    // Serialized fields
    [SerializeField] private GameObject canvas;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;
    [SerializeField] private Button option3Button;
    [SerializeField] private Button option4Button;
    [SerializeField] private GameObject resultCanvas;
    [SerializeField] private TextMeshProUGUI correctAnswer;
    [SerializeField] private TextMeshProUGUI falseAnswer;
    [SerializeField] private DoorManager doorManager; // Reference to the DoorManager script
    private DoorCombination[] doorCombinations;
    private healthSystem healthSystem;

    private int x =1;

    // Other private fields
    private QuizData quizData;
    private int count = 0;
    private int currentQuestionIndex = -1;

    void Start()
    {   

        GameObject healthSystemObject = GameObject.Find("Player");

    // Assign the healthSystem reference
        healthSystem = healthSystemObject.GetComponent<healthSystem>();


        canvas.SetActive(false);
        resultCanvas.SetActive(false);
        doorCombinations = doorManager.combinations;
        DisableAllTeleporters(); // Disable all teleporters at start
    }

    public void OnInteractButtonClicked()
    {

        string selectedAnswer = EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>().text;

        if (selectedAnswer == quizData.questions[currentQuestionIndex].answer)
        {
            resultCanvas.SetActive(true);
            StartCoroutine(HideResultCanvas());
            canvas.SetActive(false);
            correctAnswer.gameObject.SetActive(true);
            falseAnswer.gameObject.SetActive(false);
            EnableTeleportersForCurrentCombination();
            currentQuestionIndex++;
            
        }
        else
        {
            resultCanvas.SetActive(true);
            StartCoroutine(HideResultCanvas());
            correctAnswer.gameObject.SetActive(false);
            falseAnswer.gameObject.SetActive(true);
            healthSystem.TakeDamage(x);
        }
    }

    private IEnumerator HideResultCanvas()
    {
        yield return new WaitForSeconds(0.75f);
        resultCanvas.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AccessButton"))
        {
            if (currentQuestionIndex == -1)
            {
                string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Data/questions.json");
                Debug.Log("JSON string: " + jsonString);
                quizData = JsonUtility.FromJson<QuizData>(jsonString);
                currentQuestionIndex = UnityEngine.Random.Range(0, quizData.questions.Length);
            }
            questionText.text = quizData.questions[currentQuestionIndex].question;
            canvas.SetActive(true);

            string[] options = quizData.questions[currentQuestionIndex].options;
            option1Button.GetComponentInChildren<TextMeshProUGUI>().text = options[0];
            option2Button.GetComponentInChildren<TextMeshProUGUI>().text = options[1];
            option3Button.GetComponentInChildren<TextMeshProUGUI>().text = options[2];
            option4Button.GetComponentInChildren<TextMeshProUGUI>().text = options[3];
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AccessButton"))
        {
            canvas.SetActive(false);
        }
    }

    private void EnableTeleportersForCurrentCombination()
    {
        GameObject doorAccessButton = doorCombinations[count].doorAccessButton;
        foreach (DoorCombination combination in doorCombinations)
        {
            Teleporter redGateTeleporter = combination.redGate.GetComponent<Teleporter>();
            Teleporter blueGateTeleporter = combination.blueGate.GetComponent<Teleporter>();

            if (combination.doorAccessButton == doorAccessButton)
            {
                BoxCollider2D col = combination.doorAccessButton.GetComponent<BoxCollider2D>();
                redGateTeleporter.EnableTeleporter();
                blueGateTeleporter.EnableTeleporter();
                col.enabled = false;
            }
            else
            {
                redGateTeleporter.DisableTeleporter();
                blueGateTeleporter.DisableTeleporter();
            }
        }
        count++;
    }

    private void DisableAllTeleporters()
    {
        foreach (DoorCombination combination in doorCombinations)
        {
            combination.redGate.GetComponent<Teleporter>().DisableTeleporter();
            combination.blueGate.GetComponent<Teleporter>().DisableTeleporter();
        }
    }


}
