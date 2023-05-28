using UnityEngine;

public class LadderMovement : MonoBehaviour
{
    private float vertical;
    private float speed = 8f;
    private bool isLadder;
    private bool isClimbing;
    private bool isMovingDown;

    [SerializeField] private Rigidbody2D rb;

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");

        if (isLadder)
        {
            if (Mathf.Abs(vertical) > 0f)
            {
                isClimbing = true;
                isMovingDown = vertical < 0f;
            }
            else
            {
                isClimbing = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, vertical * speed);
        }
        else
        {
            rb.gravityScale = 4f;

            if (!isLadder)
            {
                // Stop moving upwards when not on the ladder
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
            else if (isMovingDown && vertical < 0f)
            {
                // Move down while the down button is pressed
                rb.velocity = new Vector2(rb.velocity.x, -speed);
            }
            else
            {
                // Stop moving down when the down button is released or when moving up
                rb.velocity = new Vector2(rb.velocity.x, 0f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            Debug.Log("Entered Ladder");
            isLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            Debug.Log("Exited ladder");
            isLadder = false;
            isClimbing = false; // Reset the climbing state when leaving the ladder
            rb.velocity = Vector2.zero; // Stop the player's movement when leaving the ladder
        }
    }
}
