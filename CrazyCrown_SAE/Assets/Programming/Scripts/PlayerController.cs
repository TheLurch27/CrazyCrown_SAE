using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public float pickupRadius = 1.5f; // Der Radius, in dem der Spieler das Item aufnehmen kann
    private Vector2 moveInput;
    private bool isSaluting = false;
    private bool canMove = false;
    private float startDelay = 0f;
    private bool gameStarted = false;

    private void Start()
    {
        if (!gameStarted)
        {
            StartGame();
            gameStarted = true;
        }
    }

    private void Update()
    {
        if (!canMove)
            return;

        if (isSaluting)
        {
            if (!(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S)))
                StopSaluting();
            else
                return;
        }

        moveInput = GetMovementInput();

        Vector3 moveDirection = new Vector3(moveInput.x, 0f, 0f);
        transform.position += moveDirection * moveSpeed * Time.deltaTime;

        if (moveDirection.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (moveDirection.x > 0)
            transform.localScale = new Vector3(1, 1, 1);

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            StartSaluting();
    }

    private Vector2 GetMovementInput()
    {
        float moveX = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ? -1f :
                      Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) ? 1f : 0f;

        return new Vector2(moveX, 0f);
    }

    private void StartSaluting()
    {
        isSaluting = true;
        animator.SetBool("Salute", true);
    }

    private void StopSaluting()
    {
        isSaluting = false;
        animator.SetBool("Salute", false);
    }

    private void StartGame()
    {
        Invoke("EnableMovement", startDelay);
    }

    private void EnableMovement()
    {
        canMove = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance <= pickupRadius)
            {
                // Führe hier die Aktionen aus, die bei der Kollision mit dem Item innerhalb des Radius ausgeführt werden sollen
                Destroy(other.gameObject);

                // Weitere Aktionen, wie das Erhöhen des Punktestands oder andere Spielmechaniken, könnten hier implementiert werden.
            }
        }
    }
}
