using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverUI;

    private void Start()
    {
        HideGameOverUI();
    }

    public void ShowGameOverUI()
    {
        Time.timeScale = 0f; // Friere die Spielzeit ein
        gameOverUI.SetActive(true);
    }

    public void HideGameOverUI()
    {
        Time.timeScale = 1f; // Setze die Spielzeit wieder auf normal
        gameOverUI.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
