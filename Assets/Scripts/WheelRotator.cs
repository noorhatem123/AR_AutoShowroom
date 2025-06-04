using UnityEngine;  // Gives access to Unity engine features like Transform and MonoBehaviour

// This script makes the car wheels rotate when activated
public class WheelRotator : MonoBehaviour
{
    public float rotationSpeed = 200f;  // Speed of wheel rotation in degrees per second
    private bool isRotating = false;    // Tracks whether the wheel should be spinning

    // Called when the user taps the "Rotate Wheels" button
    public void ToggleRotation()
    {
        isRotating = !isRotating;  // Flip the state: start or stop rotation
    }

    // Called every frame — handles the actual wheel rotation
    void Update()
    {
        if (isRotating)
        {
            // Rotate the wheel around its local X-axis (forward spinning)
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
}
