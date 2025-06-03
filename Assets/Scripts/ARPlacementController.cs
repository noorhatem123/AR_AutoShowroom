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

    // 🔁 Added to store original transform
    private Vector3 initialPosition;
    private Quaternion initialRotation;

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

            if (spawnedObject != null)
            {
                Destroy(spawnedObject);
            }

            spawnedObject = Instantiate(carPrefabs[selectedIndex], adjustedPosition, uprightRotation);

            // 🔁 Save initial transform
            initialPosition = adjustedPosition;
            initialRotation = uprightRotation;

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
            // Reset position and rotation
            spawnedObject.transform.SetPositionAndRotation(initialPosition, initialRotation);

            // Reset color
            CarColorCycler colorCycler = spawnedObject.GetComponentInChildren<CarColorCycler>();
            if (colorCycler != null)
            {
                colorCycler.ResetColor(); // make sure you implement this method
            }

            // Reset doors
            foreach (var door in spawnedObject.GetComponentsInChildren<DoorController>())
            {
                door.ResetDoor(); // make sure you implement this method
            }

            // Stop wheels
            foreach (var rotator in spawnedObject.GetComponentsInChildren<WheelRotator>())
            {
                rotator.enabled = false;
            }

            // Stop engine sound
            EngineSoundController engine = spawnedObject.GetComponentInChildren<EngineSoundController>();
            if (engine != null)
            {
                engine.StopEngine(); // implement StopEngine()
            }

            // Stop voiceover
            VoiceoverPlayer voice = spawnedObject.GetComponentInChildren<VoiceoverPlayer>();
            if (voice != null)
            {
                voice.StopVoiceover(); // implement StopVoiceover()
            }

            Debug.Log("Car fully reset to default state.");
        }
    }

}
