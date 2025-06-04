using UnityEngine;  // Access to Unity core features like Transform and Coroutine

// This script controls opening and closing a car door smoothly using rotation
public class DoorController : MonoBehaviour
{
    public bool isOpen = false;           // Tracks whether the door is open or closed
    public float openAngle = 75f;         // How far the door opens in degrees
    public float openSpeed = 2f;          // How fast the door opens/closes

    private Quaternion closedRotation;    // The original (closed) rotation of the door
    private Quaternion openRotation;      // The target rotation when the door is open

    void Start()
    {
        // Save the original rotation when the scene starts
        closedRotation = transform.localRotation;

        // Calculate what the open rotation should be, rotating around the Y axis
        openRotation = Quaternion.Euler(0, openAngle, 0) * closedRotation;
    }

    // Called to toggle (open or close) the door
    public void ToggleDoor()
    {
        isOpen = !isOpen;  // Flip the open/closed state
        StopAllCoroutines();  // Stop any ongoing door animations
        StartCoroutine(RotateDoor(isOpen ? openRotation : closedRotation));  // Rotate to open or close
    }

    // Called when resetting the car — always closes the door
    public void ResetDoor()
    {
        isOpen = false;  // Mark as closed
        StopAllCoroutines();  // Stop any ongoing animations
        StartCoroutine(RotateDoor(closedRotation));  // Smoothly rotate to the closed position
    }

    // Smoothly rotates the door to the target rotation over time
    private System.Collections.IEnumerator RotateDoor(Quaternion targetRotation)
    {
        while (Quaternion.Angle(transform.localRotation, targetRotation) > 0.5f)
        {
            // Gradually rotate the door toward the target
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * openSpeed);
            yield return null;  // Wait until next frame
        }

        // Snap to the final rotation to finish the animation cleanly
        transform.localRotation = targetRotation;
    }
}
