using UnityEngine;
// Lets us use Unity collision and object tools

public class Pickup : MonoBehaviour
// This script gives the player points when they touch the pickup
{
    public int scoreValue = 1;
    // How many points this pickup is worth

    void OnTriggerEnter(Collider other)
    {
        // Check if the object touching this pickup is the player
        if (other.CompareTag("Player"))
        {
            // Add score using the GameManager
            GameManager.instance.AddScore(scoreValue);

            // Destroy the pickup after collecting it
            Destroy(gameObject);
        }
    }
}