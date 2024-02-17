using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject audioMenuUI;
    public GameObject gameplayMenuUI;
    public GameObject creditsUI;

    private void Start()
    {
        // Beim Start wird nur das Hauptmenü angezeigt, während die anderen deaktiviert werden.
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
        audioMenuUI.SetActive(false);
        gameplayMenuUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void StartGame()
    {
        
    }

    public void ShowSettingsMenu()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }

    public void ShowAudioMenu()
    {
        settingsMenuUI.SetActive(false);
        audioMenuUI.SetActive(true);
    }

    public void ShowGameplayMenu()
    {
        settingsMenuUI.SetActive(false);
        gameplayMenuUI.SetActive(true);
    }

    public void ShowCredits()
    {
        mainMenuUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void GoBack()
    {
        ShowMainMenu();
    }
}
