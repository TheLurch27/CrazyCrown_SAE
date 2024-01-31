using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float changeDirectionInterval = 2f;
    public float minChangeInterval = 1f;
    public float maxChangeInterval = 3f;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.right;
    private float changeDirectionTimer;
    private bool facingRight = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        changeDirectionTimer = Random.Range(minChangeInterval, maxChangeInterval);
    }

    void Update()
    {
        changeDirectionTimer -= Time.deltaTime;
        if (changeDirectionTimer <= 0)
        {
            ChangeDirection();
            changeDirectionTimer = Random.Range(minChangeInterval, maxChangeInterval);
        }

        FlipCharacter();
    }

    void FixedUpdate()
    {
        MoveNPC();
    }

    void ChangeDirection()
    {
        // Es wird Random Entschieden wann die Queen die Richtung ändert
        direction = (Random.Range(0, 2) == 0) ? Vector2.left : Vector2.right;
    }

    void MoveNPC()
    {
        // Bewegung in eine Richtung
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    void FlipCharacter()
    {
        if ((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
        {
            facingRight = !facingRight;

            // Hier wird die Queen einmal umgedreht
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}
