using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMarkerlessScene()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("MarkerlessARScene");
    }

    public void LoadMarkerBasedScene()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene("MarkerBasedScene");
    }
    public void LoadStartupScene()
    {
        Resources.UnloadUnusedAssets();
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartupScene");
    }
}
