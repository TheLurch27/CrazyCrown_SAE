using UnityEngine;

public class NPCController : MonoBehaviour
{
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private bool isMovingRight = true;
    private SpriteRenderer spriteRenderer;
    public LifeDisplayManager lifeDisplayManager;

    public float raycastDistance = 5f;
    public LayerMask characterLayer;

    public Animator characterAnimator;

    private bool isCoolingDown = false;
    private float coolDownDuration = 5f;
    private float coolDownTimer = 0f;

    /// <summary>
    /// Wird beim Start des Skripts aufgerufen. Initialisiert den Rigidbody und den SpriteRenderer.
    /// </summary>
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Wird jeden Frame aufgerufen. Steuert die NPC-Bewegung und Interaktionen.
    /// </summary>
    private void Update()
    {
        if (isCoolingDown)
        {
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0f)
            {
                isCoolingDown = false;
                coolDownTimer = 0f;
            }
        }
        else
        {
            MoveNPC();

            UpdateFacingDirection();

            if (ShouldChangeDirection() && !IsCharacterSaluting())
            {
                ChangeDirection();
            }

            if (IsCharacterInSight() && !IsCharacterSaluting())
            {
                Debug.Log("Ertappt");
                StartCoolDown();
                lifeDisplayManager.OnCharacterCaught();
            }
        }
    }

    /// <summary>
    /// Hier wird der Cooldown eingestellt
    /// </summary>
    private void StartCoolDown()
    {
        isCoolingDown = true;
        coolDownTimer = coolDownDuration;
    }

    /// <summary>
    /// Hier wird geschaut ob die Richtung geändert werden muss
    /// </summary>
    /// <returns>True, wenn die Richtung geändert werden soll, sonst False.</returns>
    private bool ShouldChangeDirection()
    {
        // 0,01% 
        return Random.value < 0.001f || HitWall();
    }

    /// <summary>
    /// Überprüft ob der NPC auf eine Wand trifft
    /// </summary>
    /// <returns>True, wenn eine Wand getroffen wird, sonst False.</returns>
    private bool HitWall()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, characterLayer);
        return hit.collider != null && hit.collider.CompareTag("Wall");
    }

    /// <summary>
    /// Ändert die Richtung des NPCs
    /// </summary>
    private void ChangeDirection()
    {
        isMovingRight = !isMovingRight;
    }

    /// <summary>
    /// Bewegt den NPC
    /// </summary>
    private void MoveNPC()
    {
        Vector2 movement = isMovingRight ? Vector2.right : Vector2.left;

        rb.velocity = movement * moveSpeed;
    }

    /// <summary>
    /// Aktualisiert die Ausrichtung des NPCs
    /// </summary>
    private void UpdateFacingDirection()
    {
        spriteRenderer.flipX = !isMovingRight;
    }

    /// <summary>
    /// Überprüft, ob ein Charakter im Sichtfeld des NPCs ist
    /// </summary>
    /// <returns>True, wenn ein Charakter sichtbar ist, sonst False.</returns>
    private bool IsCharacterInSight()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, raycastDistance, characterLayer);

        return hit.collider != null;
    }

    /// <summary>
    /// Überprüft, ob der Charakter eine Salut-Animation ausführt
    /// </summary>
    /// <returns>True, wenn der Charakter salutiert, sonst False.</returns>
    private bool IsCharacterSaluting()
    {
        if (characterAnimator != null)
        {
            return characterAnimator.GetCurrentAnimatorStateInfo(0).IsName("Salute");
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Behandelt Kollisionen mit anderen Kollidern.
    /// </summary>
    /// <param name="other">Das andere Collider-Objekt, mit dem kollidiert wurde.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
        else if (other.CompareTag("Player"))
        {
            lifeDisplayManager.OnCharacterCaught();
        }
    }

    /// <summary>
    /// Zeichnet eine Art "Laser" um den RayCast sichtbar zu machen
    /// </summary>
    private void OnDrawGizmos()
    {
        Vector2 direction = isMovingRight ? Vector2.right : Vector2.left;

        Vector2 raycastStart = transform.position;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(raycastStart, raycastStart + direction * raycastDistance);
    }

    /// <summary>
    /// Hier wird dafür gesorgt das die Queen die Richtung ändert sobald sie auf eine Wand trífft.
    /// </summary>
    /// <param name="collision">Die Kollision mit der Wand</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            isMovingRight = !isMovingRight;
        }
    }
}
