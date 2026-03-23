using UnityEngine;
// Lets us use Unity movement, transforms, and collision

public class EnemyFollow : MonoBehaviour
// This script makes the enemy:
// 1. Turn to face the player
// 2. Move toward the player
// 3. Damage the player on contact
{
    public Transform player;
    // Drag the Player object into this field in the Inspector

    public float speed = 3f;
    // How fast the enemy moves

    public float rotationSpeed = 5f;
    // How fast the enemy rotates toward the player

    public float damageCooldown = 1f;
    // Time between each damage hit

    private float nextDamageTime = 0f;
    // Keeps track of when the enemy can deal damage again

    void Update()
    {
        // Stop everything if the game is over
        if (GameManager.instance != null && (GameManager.instance.isGameOver || GameManager.instance.hasWon))
        {
            return;
        }

        // Stop if player is not assigned
        if (player == null)
        {
            return;
        }

        // Get direction from enemy to player
        Vector3 direction = (player.position - transform.position).normalized;

        // ROTATE enemy to face player
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        // Creates the rotation needed to look at the player

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        // Smoothly rotates the enemy toward the player

        // MOVE enemy forward (based on its forward direction)
        transform.position += transform.forward * speed * Time.deltaTime;
        // Moves the enemy forward in the direction it's facing
    }

    void OnCollisionStay(Collision collision)
    {
        // Check if enemy is touching the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Only deal damage if cooldown has passed
            if (Time.time >= nextDamageTime)
            {
                // Damage the player
                GameManager.instance.TakeDamage(1);

                // Reset cooldown timer
                nextDamageTime = Time.time + damageCooldown;
            }
        }
    }
}