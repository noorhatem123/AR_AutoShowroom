using System.Collections.Generic;                           // Enables use of generic collections like Dictionary
using UnityEngine;                                          // Base Unity engine functionalities
using UnityEngine.XR.ARFoundation;                          // AR Foundation API
using UnityEngine.XR.ARSubsystems;                          // Subsystems like tracking state, reference images

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager trackedImageManager;      // Manages image tracking events
    public GameObject[] arPrefabs;                           // Prefabs to instantiate for each reference image

    private Dictionary<string, GameObject> spawnedPrefabs = new Dictionary<string, GameObject>();
    // Maps image names to spawned prefabs, ensures only one instance per image

    void Awake()
    {
        // Gets the ARTrackedImageManager component from the same GameObject
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Subscribes to image tracking event when the script becomes active
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        // Unsubscribes to prevent memory leaks or unwanted behavior
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    // Called automatically when a tracked image is added, updated, or removed
    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // For new tracked images
        foreach (var newImage in eventArgs.added)
        {
            string imageName = newImage.referenceImage.name;

            foreach (var prefab in arPrefabs)
            {
                // Match prefab name with reference image name
                if (prefab.name == imageName && !spawnedPrefabs.ContainsKey(imageName))
                {
                    // Instantiate prefab at image position and attach it to the image
                    GameObject go = Instantiate(prefab, newImage.transform.position, newImage.transform.rotation, newImage.transform);
                    spawnedPrefabs[imageName] = go;

                    // Link the UI to the new car if CarUIController exists
                    CarUIController uiController = FindObjectOfType<CarUIController>();
                    if (uiController != null)
                    {
                        uiController.SetCar(go);  // Connects car prefab to the UI
                        uiController.gameObject.GetComponent<Canvas>().enabled = true;  // Show the UI
                    }
                }
            }
        }

        // For updated tracked images
        foreach (var updatedImage in eventArgs.updated)
        {
            string imageName = updatedImage.referenceImage.name;

            // If prefab already spawned, toggle its visibility based on tracking state
            if (spawnedPrefabs.TryGetValue(imageName, out GameObject go))
            {
                go.SetActive(updatedImage.trackingState == TrackingState.Tracking);
            }
        }
    }
}
