using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;
    private GameObject player;
    private bool isPlayerDetected = false;

    [SerializeField] Canvas gameOverCanvas;

    private float detectionTime = 0f; // Time since player detection
    private float maxDetectionTime = 2f; // Maximum time for detection before game over

    private bool isGameOver = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointB.transform;
        anim.SetBool("isRunning", true);
        gameOverCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isGameOver)
            return; // Stop updating if game over

        // Set the playerDetected parameter in the Animator controller
        anim.SetBool("playerDetected", isPlayerDetected);

        if (isPlayerDetected)
        {
            detectionTime += Time.deltaTime; // Increment detection time

            if (detectionTime >= maxDetectionTime)
            {
                // Game over condition
                Debug.Log("Game Over");
                isGameOver = true;
                Time.timeScale = 0f; // Pause the game
                gameOverCanvas.gameObject.SetActive(true);

                
            }

            anim.SetBool("isRunning", false);
            anim.SetBool("isKicking", true);
        }
        else
        {
            detectionTime = 0f; // Reset detection time

            anim.SetBool("isRunning", true);
            anim.SetBool("isKicking", false);

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                flip();
                currentPoint = pointA.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                flip();
                currentPoint = pointB.transform;
            }
        }
    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
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
}
