using UnityEngine;
// Lets us use Unity movement, transforms, and input

public class PlayerMovement : MonoBehaviour
// This script moves the player based on the camera direction
{
    public float moveSpeed = 5f;
    // How fast the player moves

    public float rotationSpeed = 10f;
    // How quickly the player turns toward the move direction

    public Transform cameraTransform;
    // Drag the Main Camera into this field in the Inspector
  
    void Update()
    {
        if (GameManager.instance != null && (GameManager.instance.isGameOver || GameManager.instance.hasWon))
        {
            return;
        }
        // Get keyboard input
        float horizontal = Input.GetAxis("Horizontal");
        // A/D or Left/Right arrow keys

        float vertical = Input.GetAxis("Vertical");
        // W/S or Up/Down arrow keys

        // Get the camera's forward direction
        Vector3 cameraForward = cameraTransform.forward;
        // This points in the direction the camera is facing

        // Get the camera's right direction
        Vector3 cameraRight = cameraTransform.right;
        // This points to the right side of the camera

        // Remove vertical tilt so player stays moving on the ground only
        cameraForward.y = 0f;
        cameraRight.y = 0f;

        // Normalize so movement stays consistent
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Create movement direction based on camera angle
        Vector3 moveDirection = cameraForward * vertical + cameraRight * horizontal;

        // Check if the player is actually trying to move
        if (moveDirection.magnitude >= 0.1f)
        {
            // Normalize so diagonal movement is not faster
            moveDirection.Normalize();

            // Move the player
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            // Make the player face the direction they are moving
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}