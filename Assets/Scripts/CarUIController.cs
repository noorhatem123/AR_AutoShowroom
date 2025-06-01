using UnityEngine;

public class CarUIController : MonoBehaviour
{
    public void ChangeCarColor()
    {
        var colorCycler = FindObjectOfType<CarColorCycler>();
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
}
