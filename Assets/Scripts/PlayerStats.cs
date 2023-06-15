using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static float Distance;
    public static float FuelVolume;
    public static int Score;

    private void Start()
    {
        Distance = 0;
        FuelVolume = 100;
    }
}
