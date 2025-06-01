using UnityEngine;

public class WheelRotator : MonoBehaviour
{
    public float rotationSpeed = 200f;
    private bool isRotating = false;

    public void ToggleRotation()
    {
        isRotating = !isRotating;
    }

    void Update()
    {
        if (isRotating)
        {
            transform.Rotate(Vector3.right * rotationSpeed * Time.deltaTime);
        }
    }
}
