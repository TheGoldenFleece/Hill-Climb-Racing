using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Windows;

public class CarPedal : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    bool isPressed;
    [SerializeField] int coefficient;
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] Rigidbody2D rightWheelRB;
    [SerializeField] Rigidbody2D leftWheelRB;
    [SerializeField] Rigidbody2D vehicleRB;

    private void FixedUpdate()
    {
        if (isPressed)
        {
            if (PlayerStats.FuelVolume <= 0) return;

            float torque = - coefficient * speed * Time.fixedDeltaTime * 10;
            float rotationTorque = coefficient * rotationSpeed * Time.fixedDeltaTime * 10;
            rightWheelRB.AddTorque(torque);
            leftWheelRB.AddTorque(torque);
            vehicleRB.AddTorque(rotationTorque);

            PlayerStats.FuelVolume -= 5 * Time.fixedDeltaTime;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }

}
