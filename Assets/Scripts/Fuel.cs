using UnityEngine;
using UnityEngine.UI;

public class Fuel : MonoBehaviour
{
    [SerializeField] Image volume;
    [SerializeField] Gradient fuelVolumeGradient;

    void Update()
    {
        DisplayFuelVolume();
    }

    public void DisplayFuelVolume()
    {
        volume.fillAmount = PlayerStats.FuelVolume / 100;
        volume.color = fuelVolumeGradient.Evaluate(volume.fillAmount);
    }
}
