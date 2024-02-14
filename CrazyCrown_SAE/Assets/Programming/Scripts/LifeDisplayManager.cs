using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LifeDisplayManager : MonoBehaviour
{
    public GameObject[] lifeIcons;
    public GameObject[] goneLifeIcons;
    public GameObject gameOverUI; // Referenz auf das GameOverUI-Objekt
    private int currentLife = 3;
    private bool isGameOver = false;

    public UnityEvent characterCaughtEvent;

    private void Start()
    {
        ShowAllLifeIcons();
        HideAllGoneLifeIcons();
        gameOverUI.SetActive(false); // Deaktiviere das GameOverUI am Anfang
    }

    public void LoseLife()
    {
        if (isGameOver) return; // Wenn das Spiel bereits vorbei ist, tue nichts

        currentLife--;

        UpdateLifeDisplay();

        if (currentLife == 0)
        {
            ShowGameOverUI(); // Zeige das GameOverUI, wenn alle Leben verloren wurden
        }
    }

    private void UpdateLifeDisplay()
    {
        for (int i = lifeIcons.Length - 1; i >= currentLife; i--)
        {
            lifeIcons[i].SetActive(false);
            goneLifeIcons[i].SetActive(true);
        }
    }

    private void ShowAllLifeIcons()
    {
        foreach (var lifeIcon in lifeIcons)
        {
            lifeIcon.SetActive(true);
        }
    }

    private void HideAllGoneLifeIcons()
    {
        foreach (var goneLifeIcon in goneLifeIcons)
        {
            goneLifeIcon.SetActive(false);
        }
    }

    private void ShowGameOverUI()
    {
        isGameOver = true; // Setze den Spielstatus auf GameOver
        gameOverUI.SetActive(true); // Aktiviere das GameOverUI
        Time.timeScale = 0f; // Friere die Spielzeit ein
    }

    public void OnCharacterCaught()
    {
        LoseLife();
        Debug.Log("Character caught! Life lost!");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Setze die Spielzeit wieder auf normal
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
