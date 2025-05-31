using UnityEngine;

public class CarUIController : MonoBehaviour
{
    public CarColorCycler colorCycler;

    public void ChangeCarColor()
    {
        if (colorCycler != null)
        {
            colorCycler.CycleColor();
        }
    }
}
