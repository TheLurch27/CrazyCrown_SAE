using UnityEngine;

public class ButlerController : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float moveSpeed = 2f; // Bewegungsgeschwindigkeit des NPCs
    public float returnTime = 10f; // Zeit, nach der der NPC zerstört wird, nachdem er zurückgekehrt ist
    public float distanceAhead = 2f; // Entfernung, die der NPC vor dem Spieler stehen bleiben soll
    public float waitTime = 3f; // Wartezeit beim Spieler

    private bool returning = false; // Flag, um zu überprüfen, ob der NPC auf dem Rückweg ist
    private Vector3 startPosition; // Startposition des NPCs
    private float waitTimer = 0f; // Timer für die Wartezeit beim Spieler
    private SpriteRenderer spriteRenderer; // Referenz auf den SpriteRenderer

    // Variable, um den Zustand des Spielerbildes zu speichern
    private bool playerImageStopped = false;

    void Start()
    {
        startPosition = transform.position; // Speichern der Startposition
        spriteRenderer = GetComponent<SpriteRenderer>(); // Holen des SpriteRenderer-Komponenten
    }

    void Update()
    {
        if (!returning)
        {
            // Bewegung zum Spieler, nur wenn das Spielerbild nicht angehalten ist
            if (!playerImageStopped)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // Überprüfen, ob der NPC den Spieler erreicht hat
            if (Vector3.Distance(transform.position, player.position) <= distanceAhead)
            {
                // NPC-Dialog oder Aktion hier einfügen

                // Starte den Timer für die Wartezeit beim Spieler
                waitTimer = waitTime;
                returning = true;
            }

            // Anpassen der Richtung des NPCs basierend auf seiner Bewegung
            if (transform.position.x < player.position.x)
            {
                Flip(false); // Nach rechts schauen
            }
            else
            {
                Flip(true); // Nach links schauen
            }
        }
        else
        {
            // Wartezeit beim Spieler
            if (waitTimer > 0f)
            {
                waitTimer -= Time.deltaTime;
            }
            else
            {
                // Starte den Rückweg zum Startpunkt
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

                // Überprüfen, ob der NPC zur Startposition zurückgekehrt ist
                if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                {
                    // Zerstöre den NPC, nachdem er zur Startposition zurückgekehrt ist
                    Destroy(gameObject);
                }
            }

            // Anpassen der Richtung des NPCs basierend auf seiner Rückkehr
            if (transform.position.x < startPosition.x)
            {
                Flip(false); // Nach rechts schauen
            }
            else
            {
                Flip(true); // Nach links schauen
            }
        }
    }

    // Methode zum Spiegeln des Bildes des NPCs
    private void Flip(bool facingLeft)
    {
        spriteRenderer.flipX = facingLeft;
    }

    // Methode, um den Zustand des Spielerbildes zu aktualisieren
    public void SetPlayerImageStopped(bool stopped)
    {
        playerImageStopped = stopped;
    }
}
