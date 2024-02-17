using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string sceneToLoad; // Name der Szene, zu der die T�r f�hrt
    public float interactionRadius = 2f; // Radius, in dem die T�r interagierbar ist
    public GameObject messageUI; // Das UI-Element f�r die Nachricht

    private bool isPlayerInRange; // Gibt an, ob der Spieler im Interaktionsradius ist

    void Update()
    {
        // Wenn der Spieler sich im Interaktionsradius befindet und die Interaktionstaste gedr�ckt wird (z.B. "E")
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Lade die neue Szene
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // �berpr�fe, ob das kollidierende Objekt der Spieler ist
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
        // �berpr�fe, ob das kollidierende Objekt der Spieler ist
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            // Deaktiviere die UI-Nachricht
            if (messageUI != null)
                messageUI.SetActive(false);
        }
    }
}
