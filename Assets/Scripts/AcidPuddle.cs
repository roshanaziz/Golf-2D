using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPuddle : MonoBehaviour
{
    public GameObject ballPrefab;  // Reference to the ball prefab
    public Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Collision Started");
        originalPosition = new Vector3(-1, 3, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the ball
        if (other.gameObject.CompareTag("Player"))
        {
            // Destroy the ball
            Destroy(other.gameObject);

            // Instantiate a new ball at the original position
            Instantiate(ballPrefab, originalPosition, Quaternion.identity);
        }
    }

}
