using UnityEngine;                         // Provides access to Unity-specific classes and functions
using UnityEngine.SceneManagement;        // Allows working with scenes (loading, switching, etc.)

// This script handles scene transitions between different AR modes and the startup menu
public class SceneLoader : MonoBehaviour
{
    // Loads the markerless AR scene (uses plane detection)
    public void LoadMarkerlessScene()
    {
        Resources.UnloadUnusedAssets();                     // Frees unused memory to improve performance
        SceneManager.LoadScene("MarkerlessARScene");        // Loads the scene named "MarkerlessARScene"
    }

    // Loads the marker-based AR scene (uses image tracking)
    public void LoadMarkerBasedScene()
    {
        Resources.UnloadUnusedAssets();                     // Frees unused memory before scene switch
        SceneManager.LoadScene("MarkerBasedScene");         // Loads the scene named "MarkerBasedScene"
    }

    // Loads the startup or main menu scene
    public void LoadStartupScene()
    {
        Resources.UnloadUnusedAssets();                                         // Clean up unused assets
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartupScene");     // Load the startup scene
    }
}
