using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float launchForce = 40f;
    public Transform launchDirection;
    public float delayBeforeLaunch = 0.5f;

    private bool isBallInside = false;
    private Rigidbody2D ballRb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            ballRb = other.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                isBallInside = true;
                ballRb.velocity = Vector2.zero;
                ballRb.angularVelocity = 0f;
                ballRb.isKinematic = true;
                other.transform.position = transform.position;
                StartCoroutine(LaunchBallAfterDelay(ballRb));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ball"))
        {
            isBallInside = false;
        }
    }

    private IEnumerator LaunchBallAfterDelay(Rigidbody2D ballRb)
    {
        yield return new WaitForSeconds(delayBeforeLaunch);

        if (isBallInside && ballRb != null)
        {
            ballRb.isKinematic = false;

            Vector2 launchDir = (launchDirection.position - transform.position).normalized;

            ballRb.AddForce(launchDir * launchForce, ForceMode2D.Impulse);
            isBallInside = false;
        }
    }
}
