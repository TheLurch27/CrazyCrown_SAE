using UnityEngine;

public class NPCController : MonoBehaviour
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
    private bool facingRight = true; // Flag, um die Richtung des NPCs zu verfolgen

    void Start()
    {
        startPosition = transform.position; // Speichern der Startposition
        spriteRenderer = GetComponent<SpriteRenderer>(); // Holen des SpriteRenderer-Komponenten
    }

    void Update()
    {
        if (!returning)
        {
            // Bewegung zum Spieler
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);

            // �berpr�fen, ob der NPC den Spieler erreicht hat
            if (Vector3.Distance(transform.position, player.position) <= distanceAhead)
            {
                // NPC-Dialog oder Aktion hier einf�gen

                // Starte den Timer f�r die Wartezeit beim Spieler
                waitTimer = waitTime;
                returning = true;
            }

            // �berpr�fen und Anpassen der Richtung des NPCs
            if (transform.position.x < player.position.x && !facingRight)
            {
                Flip();
            }
            else if (transform.position.x > player.position.x && facingRight)
            {
                Flip();
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
        }
    }

    // Methode zum Spiegeln des Bildes des NPCs
    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
