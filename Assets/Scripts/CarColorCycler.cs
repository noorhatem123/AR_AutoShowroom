using UnityEngine;  // Core Unity functions

// This script lets you cycle through different paint colors for the car
public class CarColorCycler : MonoBehaviour
{
    [Tooltip("Assign all paint materials you want to cycle through.")]
    public Material[] paintOptions;  // List of all color materials (e.g., red, blue, black)

    [Tooltip("Assign all MeshRenderers (e.g., body, doors) to be recolored.")]
    public MeshRenderer[] carRenderers;  // The parts of the car to apply the paint to

    private int currentIndex = 0;       // Tracks which paint is currently applied
    private int originalIndex = 0;      // Stores the default (original) color index

    void Start()
    {
        // Load saved paint index from previous session (if any)
        if (PlayerPrefs.HasKey("CarColorIndex"))
        {
            currentIndex = PlayerPrefs.GetInt("CarColorIndex");
        }
        else
        {
            currentIndex = originalIndex;  // Start with default if no saved value
        }

        ApplyColor(currentIndex);  // Apply the current paint to the car
    }

    // Called when the user taps the "Change Color" button
    public void CycleColor()
    {
        // Safety check
        if (paintOptions.Length == 0 || carRenderers.Length == 0)
        {
            Debug.LogWarning("Paint options or renderers not assigned.");
            return;
        }

        // Move to the next color (loop back to 0 if at the end)
        currentIndex = (currentIndex + 1) % paintOptions.Length;

        // Apply the new color
        ApplyColor(currentIndex);

        // Save the selected color index so it persists between sessions
        PlayerPrefs.SetInt("CarColorIndex", currentIndex);
        PlayerPrefs.Save();
    }

    // Resets the car to its original color
    public void ResetColor()
    {
        currentIndex = originalIndex;
        ApplyColor(currentIndex);

        PlayerPrefs.SetInt("CarColorIndex", originalIndex);
        PlayerPrefs.Save();
    }

    // Actually applies the paint to all target mesh renderers
    private void ApplyColor(int index)
    {
        if (paintOptions.Length == 0 || carRenderers.Length == 0)
            return;

        foreach (MeshRenderer renderer in carRenderers)
        {
            renderer.material = paintOptions[index];
        }
    }
}
