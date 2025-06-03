using UnityEngine;

public class CarColorCycler : MonoBehaviour
{
    [Tooltip("Assign all paint materials you want to cycle through.")]
    public Material[] paintOptions;

    [Tooltip("Assign all MeshRenderers (e.g., body, doors) to be recolored.")]
    public MeshRenderer[] carRenderers;

    private int currentIndex = 0;
    private int originalIndex = 0; // always reset to this

    void Start()
    {
        // Load saved color if exists, otherwise use original
        if (PlayerPrefs.HasKey("CarColorIndex"))
        {
            currentIndex = PlayerPrefs.GetInt("CarColorIndex");
        }
        else
        {
            currentIndex = originalIndex;
        }

        ApplyColor(currentIndex);
    }

    // Called from the UI button
    public void CycleColor()
    {
        if (paintOptions.Length == 0 || carRenderers.Length == 0)
        {
            Debug.LogWarning("Paint options or renderers not assigned.");
            return;
        }

        // Advance to the next material
        currentIndex = (currentIndex + 1) % paintOptions.Length;

        ApplyColor(currentIndex);

        // Save color index to PlayerPrefs
        PlayerPrefs.SetInt("CarColorIndex", currentIndex);
        PlayerPrefs.Save();
    }

    public void ResetColor()
    {
        currentIndex = originalIndex;
        ApplyColor(currentIndex);

        // Optional: reset saved state too
        PlayerPrefs.SetInt("CarColorIndex", originalIndex);
        PlayerPrefs.Save();
    }

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
