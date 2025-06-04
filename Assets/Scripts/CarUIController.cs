using UnityEngine;  // Unity core features

// This script connects the UI buttons to various car functions (color, doors, engine, etc.)
public class CarUIController : MonoBehaviour
{
    private CarColorCycler colorCycler;           // For changing car color
    private GameObject doorFL, doorFR, doorRL, doorRR; // References to each door GameObject
    private EngineSoundController engineSound;    // Controls engine audio
    private VoiceoverPlayer voiceover;            // Controls voiceover playback

    [Header("AR Placement Reference")]
    public ARPlacementController placementController;  // Needed to switch or reset cars

    // Called when a new car is spawned — connects all features to this car
    public void SetCar(GameObject car)
    {
        colorCycler = car.GetComponentInChildren<CarColorCycler>();
        doorFL = car.transform.Find("door_fl")?.gameObject;
        doorFR = car.transform.Find("door_fr")?.gameObject;
        doorRL = car.transform.Find("door_rl")?.gameObject;
        doorRR = car.transform.Find("door_rr")?.gameObject;
        engineSound = car.GetComponentInChildren<EngineSoundController>();
        voiceover = car.GetComponentInChildren<VoiceoverPlayer>();
    }

    // UI Button: Changes the car's color
    public void ChangeCarColor()
    {
        colorCycler?.CycleColor();
    }

    // UI Button: Toggles all wheels to rotate or stop
    public void RotateWheels()
    {
        WheelRotator[] rotators = FindObjectsOfType<WheelRotator>();
        foreach (WheelRotator rotator in rotators)
        {
            rotator.ToggleRotation();
        }
    }

    // UI Buttons: Open/close each door individually
    public void ToggleDoorFL() => ToggleDoor(doorFL);
    public void ToggleDoorFR() => ToggleDoor(doorFR);
    public void ToggleDoorRL() => ToggleDoor(doorRL);
    public void ToggleDoorRR() => ToggleDoor(doorRR);

    // Helper method: toggles a door if it exists
    private void ToggleDoor(GameObject door)
    {
        if (door != null)
            door.GetComponent<DoorController>()?.ToggleDoor();
    }

    // UI Button: Starts/stops engine sound
    public void ToggleEngine()
    {
        engineSound?.ToggleEngine();
    }

    // UI Button: Plays voiceover audio
    public void PlayVoiceover()
    {
        voiceover?.PlayVoiceover();
    }

    // UI Buttons: Select between Car A or Car B (index 0 or 1)
    public void SelectCarA()
    {
        placementController?.SelectCar(0);
    }

    public void SelectCarB()
    {
        placementController?.SelectCar(1);
    }

    // UI Button: Resets the car to original state
    public void ResetCar()
    {
        if (placementController != null)
            placementController.ResetPlacement();
    }
}
