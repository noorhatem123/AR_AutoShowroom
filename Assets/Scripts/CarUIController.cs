using UnityEngine;

public class CarUIController : MonoBehaviour
{
    private CarColorCycler colorCycler;
    private GameObject doorFL;
    private GameObject doorFR;
    private EngineSoundController engineSound;

    // 👇 Add this to link to ARPlacementController
    [Header("AR Placement Reference")]
    public ARPlacementController placementController;

    public void SetCar(GameObject car)
    {
        colorCycler = car.GetComponentInChildren<CarColorCycler>();
        doorFL = car.transform.Find("door_fl")?.gameObject;
        doorFR = car.transform.Find("door_fr")?.gameObject;
        engineSound = car.GetComponentInChildren<EngineSoundController>();
    }

    public void ChangeCarColor()
    {
        if (colorCycler != null)
            colorCycler.CycleColor();
    }

    public void RotateWheels()
    {
        WheelRotator[] rotators = FindObjectsOfType<WheelRotator>();
        foreach (WheelRotator rotator in rotators)
        {
            rotator.ToggleRotation();
        }
    }

    public void ToggleDoorFL()
    {
        if (doorFL != null)
            doorFL.GetComponent<DoorController>()?.ToggleDoor();
    }

    public void ToggleDoorFR()
    {
        if (doorFR != null)
            doorFR.GetComponent<DoorController>()?.ToggleDoor();
    }

    public void ToggleEngine()
    {
        engineSound?.ToggleEngine();
    }

    // 👇 New methods to select cars via UI
    public void SelectCarA()
    {
        if (placementController != null)
            placementController.SelectCar(0);
    }

    public void SelectCarB()
    {
        if (placementController != null)
            placementController.SelectCar(1);
    }
    public void ResetCar()
{
    placementController?.ResetPlacement();
}

}
