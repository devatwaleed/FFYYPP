using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float startAngle = -5.4f;
    [SerializeField] private float endAngle = 170f;
    [SerializeField] private float rotationTime = 2f;
    [SerializeField] private float waitTime = 5f;

    [SerializeField] Canvas gameOverCanvas;

    private bool isRotating = false;
    public PolygonCollider2D detectionCollider;
    private bool isCameraEnabled = true;
    private bool isPlayerDetected = false;
    private float detectionTime = 0f; // Time since player detection
    private float maxDetectionTime = 2f; // Maximum time for detection before game over
    private bool isGameOver = false;

    private void Start(){
        gameOverCanvas.gameObject.SetActive(false);

    }

    private void Update()
    {
        if (isGameOver)
            return; // Stop updating if game over

        if (isCameraEnabled && !isRotating)
        {
            StartCoroutine(RotateCoroutine(startAngle, endAngle, rotationTime));
        }

        if (isPlayerDetected)
        {
            detectionTime += Time.deltaTime; // Increment detection time

            if (detectionTime >= maxDetectionTime)
            {
                // Game over condition
                Debug.Log("Game Over");
                isGameOver = true;
                Time.timeScale = 0f; // Pause the game
                // Implement your game over logic here
                gameOverCanvas.gameObject.SetActive(true);

            }

            Debug.Log("Player Detected");
        }
        else
        {
            detectionTime = 0f; // Reset detection time
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerDetected = false;
        }
    }

    public void DisableCamera()
    {
        isCameraEnabled = false;
        detectionCollider.enabled = false;
        StopCoroutine(RotateCoroutine(startAngle, endAngle, rotationTime));
    }

    private IEnumerator RotateCoroutine(float startAngle, float endAngle, float rotationTime)
    {
        isRotating = true;

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(startRotation.eulerAngles.x, endAngle, startRotation.eulerAngles.z);

        float elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRotation;

        yield return new WaitForSeconds(waitTime);

        Quaternion returnRotation = Quaternion.Euler(startRotation.eulerAngles.x, startAngle, startRotation.eulerAngles.z);

        elapsedTime = 0f;

        while (elapsedTime < rotationTime)
        {
            transform.rotation = Quaternion.Slerp(endRotation, returnRotation, (elapsedTime / rotationTime));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = returnRotation;

        yield return new WaitForSeconds(waitTime);

        isRotating = false;
    }
}
