using UnityEngine;
// Lets us use Unity tools like transforms, vectors, and mouse input

public class CameraFollow : MonoBehaviour
// This script makes the camera orbit around the player and follow smoothly
{
    public Transform player;
    // Drag the Player object into this field in the Inspector

    public float mouseSensitivity = 200f;
    // Controls how fast the camera rotates when moving the mouse

    public float distanceFromPlayer = 6f;
    // How far the camera stays behind the player

    public float heightOffset = 2f;
    // How high the camera sits above the player

    public float smoothSpeed = 10f;
    // How smoothly the camera moves to its new position

    private float yaw = 0f;
    // Left and right camera rotation

    private float pitch = 20f;
    // Up and down camera rotation

    public float minPitch = -10f;
    // Lowest angle the camera can look

    public float maxPitch = 60f;
    // Highest angle the camera can look

    void Start()
    {
        // Locks the mouse cursor in the game window so the mouse controls the camera
        Cursor.lockState = CursorLockMode.Locked;

        // Makes the cursor invisible while playing
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Stop the script if no player is assigned
        if (player == null)
        {
            return;
        }

        // Get mouse movement
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate camera left/right
        yaw += mouseX;

        // Rotate camera up/down
        pitch -= mouseY;

        // Prevent camera from going too far up or down
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Create rotation based on mouse movement
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);

        // Create the position the camera should move to
        Vector3 targetPosition = player.position + Vector3.up * heightOffset - rotation * Vector3.forward * distanceFromPlayer;
        // Vector3.up * heightOffset = raises camera above player
        // rotation * Vector3.forward = direction camera looks
        // subtracting that puts camera behind the player

        // Smoothly move camera to the target position
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        // Make the camera always look at the player
        transform.LookAt(player.position + Vector3.up * heightOffset);
    }
}