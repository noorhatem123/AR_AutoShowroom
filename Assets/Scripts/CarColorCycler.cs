using UnityEngine;

public class CarColorCycler : MonoBehaviour
{
    [Tooltip("Assign all paint materials you want to cycle through.")]
    public Material[] paintOptions;

    [Tooltip("Assign all MeshRenderers (e.g., body, doors) to be recolored.")]
    public MeshRenderer[] carRenderers;

    private int currentIndex = 0;

    // This method will be called from the UI button
    public void CycleColor()
    {
        if (paintOptions.Length == 0 || carRenderers.Length == 0)
        {
            Debug.LogWarning("Paint options or renderers not assigned.");
            return;
        }

        // Advance to the next material
        currentIndex = (currentIndex + 1) % paintOptions.Length;

        // Apply the selected material to all assigned renderers
        foreach (MeshRenderer renderer in carRenderers)
        {
            renderer.material = paintOptions[currentIndex];
        }
    }
}
