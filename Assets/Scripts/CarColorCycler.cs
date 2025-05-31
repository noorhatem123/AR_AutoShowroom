using UnityEngine;

public class CarColorCycler : MonoBehaviour
{
    [Tooltip("Assign all paint materials you want to cycle through.")]
    public Material[] paintOptions;

    [Tooltip("Assign the Mesh Renderer of the car body.")]
    public MeshRenderer carRenderer;

    private int currentIndex = 0;

    // This method will be called from the UI button
    public void CycleColor()
    {
        if (paintOptions.Length == 0 || carRenderer == null)
        {
            Debug.LogWarning("Paint options or car renderer is not assigned.");
            return;
        }

        // Advance to the next material
        currentIndex = (currentIndex + 1) % paintOptions.Length;

        // Apply it to the car
        carRenderer.material = paintOptions[currentIndex];
    }
}
