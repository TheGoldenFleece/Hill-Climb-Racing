using UnityEngine;

public class Driver : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Ground")) return;

        GameManager.Instance.GameOver();
    }
}
