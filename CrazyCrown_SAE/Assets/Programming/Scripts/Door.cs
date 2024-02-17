using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneToLoad; // Name der Szene, zu der die Tür führt
    public float interactionRadius = 2f; // Radius, in dem die Tür interagierbar ist
    public GameObject messageUI; // Das UI-Element für die Nachricht

    private bool isPlayerInRange; // Gibt an, ob der Spieler im Interaktionsradius ist

    void Update()
    {
        // Wenn der Spieler sich im Interaktionsradius befindet und die Interaktionstaste gedrückt wird (z.B. "E")
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Lade die neue Szene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfe, ob das kollidierende Objekt der Spieler ist
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            // Aktiviere die UI-Nachricht
            if (messageUI != null)
                messageUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Überprüfe, ob das kollidierende Objekt der Spieler ist
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Deaktiviere die UI-Nachricht
            if (messageUI != null)
                messageUI.SetActive(false);
        }
    }
}
