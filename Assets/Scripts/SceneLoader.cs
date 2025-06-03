using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMarkerlessScene()
    {
        SceneManager.LoadScene("MarkerlessARScene");
    }

    public void LoadMarkerBasedScene()
    {
        SceneManager.LoadScene("MarkerBasedScene");
    }
}
