using UnityEngine;

public class CarUIController : MonoBehaviour
{
    private CarColorCycler colorCycler;
    private GameObject doorFL;
    private GameObject doorFR;

    public void SetCar(GameObject car)
    {
        colorCycler = car.GetComponentInChildren<CarColorCycler>();
        doorFL = car.transform.Find("door_fl")?.gameObject;
        doorFR = car.transform.Find("door_fr")?.gameObject;
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
}
