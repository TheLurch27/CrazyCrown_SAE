using UnityEngine;

public class ButlerController : MonoBehaviour
{
    public Transform player; // Referenz auf den Spieler
    public float moveSpeed = 2f; // Bewegungsgeschwindigkeit des NPCs
    public float returnTime = 10f; // Zeit, nach der der NPC zerst�rt wird, nachdem er zur�ckgekehrt ist
    public float distanceAhead = 2f; // Entfernung, die der NPC vor dem Spieler stehen bleiben soll
    public float waitTime = 3f; // Wartezeit beim Spieler

    private bool returning = false; // Flag, um zu �berpr�fen, ob der NPC auf dem R�ckweg ist
    private Vector3 startPosition; // Startposition des NPCs
    private float waitTimer = 0f; // Timer f�r die Wartezeit beim Spieler
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

            // �berpr�fen, ob der NPC den Spieler erreicht hat
            if (Vector3.Distance(transform.position, player.position) <= distanceAhead)
            {
                // NPC-Dialog oder Aktion hier einf�gen

                // Starte den Timer f�r die Wartezeit beim Spieler
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
                // Starte den R�ckweg zum Startpunkt
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);

                // �berpr�fen, ob der NPC zur Startposition zur�ckgekehrt ist
                if (Vector3.Distance(transform.position, startPosition) <= 0.1f)
                {
                    // Zerst�re den NPC, nachdem er zur Startposition zur�ckgekehrt ist
                    Destroy(gameObject);
                }
            }

            // Anpassen der Richtung des NPCs basierend auf seiner R�ckkehr
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
