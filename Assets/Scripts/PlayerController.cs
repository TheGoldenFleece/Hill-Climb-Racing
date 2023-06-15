using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rightWheelRB;
    [SerializeField] Rigidbody2D leftWheelRB;
    [SerializeField] Rigidbody2D vehicleRB;
    public float speed = 200f;
    float rotationSpeed = 300f;

    public static PlayerController instance;

    [SerializeField] Transform com;

    private void Awake()
    {
        if (instance != null) return;
        instance = this;
    }

    private void Start()
    {
        vehicleRB.centerOfMass = com.localPosition;
    }

    private void Update()
    {
        PlayerStats.Distance = transform.position.x;
        PlayerStats.Score = transform.position.x > 0? (int)transform.position.x : 0;

       
        if (PlayerStats.FuelVolume <= 0 && vehicleRB.IsSleeping())
        {
            GameManager.Instance.GameOver();
        }

        if (transform.position.y < -10)
        {
            GameManager.Instance.GameOver();
        }
    }
    private void FixedUpdate()
    {
        float input = -Input.GetAxisRaw("Horizontal");

        rightWheelRB.AddTorque(input * speed * Time.fixedDeltaTime);
        leftWheelRB.AddTorque(input * speed * Time.fixedDeltaTime);

        vehicleRB.AddTorque(input * rotationSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Fuel")) return;

        PlayerStats.FuelVolume = Mathf.Clamp(PlayerStats.FuelVolume + Random.Range(15, 25), 0, 100);
        Destroy(collision.gameObject);
    }
}