using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject pauseMenuUI;
    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;

        Time.timeScale = 1.0f;
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void DisplayPauseMenuUI()
    {
        Toggle();
    }

    public void Toggle()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        Time.timeScale = pauseMenuUI.activeSelf ? 1 : 0;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
