using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle_new : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 originalPosition;  // Original position to reset the ball to
    private GameObject ball;  // Reference to the ball

    void Start()
    {
        // Set the original position of the ball (you can set this manually or dynamically)
        originalPosition = new Vector3(-1, 3, 0); // Replace with your original position
        
        // Find the ball in the scene (assumes there is only one ball in the scene)
        ball = GameObject.FindWithTag("Player");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the ball
        if (other.gameObject.CompareTag("Player"))
        {
            // Reset the ball's position to the original position
            other.transform.position = originalPosition;

            // Reset the ball's velocity to zero
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }
    }
    
}
