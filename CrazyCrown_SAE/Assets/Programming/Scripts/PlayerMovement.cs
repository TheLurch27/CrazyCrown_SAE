using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    private Vector2 moveInput;
    private bool isSaluting = false;
    private bool canMove = false;
    private float startDelay = 10f; // Verzögerung in Sekunden, bevor der Spieler den Charakter bewegen kann
    private bool gameStarted = false; // Überprüfung, ob das Spiel bereits gestartet wurde

    private void Start()
    {
        // Überprüfe, ob das Spiel bereits gestartet wurde
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
            if (!(Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed))
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

        if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
            StartSaluting();
    }

    private Vector2 GetMovementInput()
    {
        float moveX = Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed ? -1f :
                      Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed ? 1f : 0f;

        return new Vector2(moveX, 0f);
    }

    private void StartSaluting()
    {
        isSaluting = true;
        animator.SetBool("Saluting", true);
    }

    private void StopSaluting()
    {
        isSaluting = false;
        animator.SetBool("Saluting", false);
    }

    private void StartGame()
    {
        Invoke("EnableMovement", startDelay); // Starte eine Verzögerung, bevor der Spieler die Kontrolle übernehmen kann
    }

    private void EnableMovement()
    {
        canMove = true;
    }
}
