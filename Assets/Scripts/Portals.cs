using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{

    public Transform exitPortal; // Reference to the exit portal
    private bool isTeleporting = false; // To prevent recursive teleportation

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTeleporting && other.CompareTag("Player"))
        {
            StartCoroutine(Teleport(other));
        }
    }

    private IEnumerator Teleport(Collider2D player)
    {
        isTeleporting = true;

        // Teleport the player to the exit portal's position
        player.transform.position = exitPortal.position;

        // Wait for a short duration to prevent immediate re-teleportation
        yield return new WaitForSeconds(0.1f);

        isTeleporting = false;
    }
}
