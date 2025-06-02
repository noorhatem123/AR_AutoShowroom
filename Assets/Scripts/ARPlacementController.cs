using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacementController : MonoBehaviour
{
    [Tooltip("Assign both car prefabs here (e.g., Car A at index 0, Car B at index 1)")]
    public GameObject[] carPrefabs;

    private int selectedIndex = -1; // No car selected by default
    private GameObject spawnedObject;
    private ARRaycastManager raycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private bool isPlaced = false;

    void Awake()
    {
        raycastManager = GetComponent<ARRaycastManager>();
    }

    // Called by UI buttons to switch car type
    public void SelectCar(int index)
    {
        selectedIndex = index;
        isPlaced = false;
        Debug.Log("Selected car index: " + selectedIndex);
    }

    void Update()
    {
        if (isPlaced || selectedIndex < 0)
            return;

        Vector2 touchPosition;

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            touchPosition = Input.mousePosition;
        }
        else
        {
            return;
        }
#else
        if (Input.touchCount == 0)
            return;

        touchPosition = Input.GetTouch(0).position;
#endif

        if (raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;

            Vector3 adjustedPosition = hitPose.position;
            adjustedPosition.y = 0.1f;

            Quaternion uprightRotation = Quaternion.Euler(0, hitPose.rotation.eulerAngles.y, 0);

            spawnedObject = Instantiate(carPrefabs[selectedIndex], adjustedPosition, uprightRotation);
            isPlaced = true;

            Debug.Log("Car placed at: " + adjustedPosition);

            CarUIController carUI = FindObjectOfType<CarUIController>();
            if (carUI != null)
            {
                carUI.SetCar(spawnedObject);
            }
        }
    }

    public void ResetPlacement()
    {
        if (spawnedObject != null)
        {
            Destroy(spawnedObject);
            spawnedObject = null;
            isPlaced = false;
            Debug.Log("Car reset.");
        }
    }
}
