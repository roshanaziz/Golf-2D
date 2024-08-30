using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotMechanic : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed = 100f; // Movement speed of the ball
    private Rigidbody2D rb;
    void Start()
    {
     Debug.Log("Code Started");   
     rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = Vector2.zero;

        // Check for input and set movement direction
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x -= 1;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x += 1;
        }

        // Normalize movement to prevent faster diagonal movement
        if (movement != Vector2.zero)
        {
            movement.Normalize();
        }

        // Apply movement to the ball
        rb.velocity = movement * moveSpeed;
        
    }
}
