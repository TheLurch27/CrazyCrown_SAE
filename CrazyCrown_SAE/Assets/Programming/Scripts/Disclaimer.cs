using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShowImageAfterDelay : MonoBehaviour
{
    public Image inputActionImage; // Das UI Image, das nach einer Verzögerung eingeblendet werden soll
    public float delayTime = 3f; // Die Verzögerungszeit in Sekunden
    public string sceneToLoad; // Die Name der Szene, die geladen werden soll, wenn der Spieler Enter drückt

    private bool hasDelayPassed = false;

    void Start()
    {
        // Deaktiviere das UI Image zu Beginn
        inputActionImage.enabled = false;

        // Rufe die Funktion ShowDelayedImage nach delayTime Sekunden auf
        Invoke("ShowDelayedImage", delayTime);
    }

    void ShowDelayedImage()
    {
        // Aktiviere das UI Image
        inputActionImage.enabled = true;
        hasDelayPassed = true;
    }

    void Update()
    {
        // Überprüfe, ob die Verzögerung abgelaufen ist und der Spieler Enter drückt und das Bild aktiv ist
        if (hasDelayPassed && inputActionImage.enabled && Input.GetKeyDown(KeyCode.Return))
        {
            // Lade die neue Szene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
