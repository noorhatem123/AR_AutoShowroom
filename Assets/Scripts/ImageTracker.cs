using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;
    public GameObject[] arPrefabs;
    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            string imageName = newImage.referenceImage.name;

            foreach (var prefab in arPrefabs)
            {
                if (prefab.name == imageName && !spawnedPrefabs.ContainsKey(imageName))
                {
                    GameObject go = Instantiate(prefab, newImage.transform.position, newImage.transform.rotation, newImage.transform);
                    spawnedPrefabs[imageName] = go;

                    CarUIController uiController = FindObjectOfType<CarUIController>();
                    if (uiController != null)
                    {
                        uiController.SetCar(go);
                        uiController.gameObject.GetComponent<Canvas>().enabled = true;
                    }
                }
            }
        }

        foreach (var updatedImage in eventArgs.updated)
        {
            string imageName = updatedImage.referenceImage.name;

            if (spawnedPrefabs.TryGetValue(imageName, out GameObject go))
            {
                go.SetActive(updatedImage.trackingState == TrackingState.Tracking);
            }
        }
    }
}
