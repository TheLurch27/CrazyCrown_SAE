using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controls : MonoBehaviour
{
    public InputActionReference movementAction;
    public InputActionReference saluteAction;

    public float moveSpeed = 5f;
    public Animator animator;
    private bool facingRight = true;
    private bool isSaluting = false;

    private void Update()
    {
        // Bewegung
        Vector2 moveInput = movementAction.action.ReadValue<Vector2>();
        Move(moveInput);

        // Blickrichtung anpassen
        FlipCharacter(moveInput.x);

        // Salutieren
        if (isSaluting)
        {
            if (!IsSalutingInput())
            {
                StopSalute();
            }
        }
        else if (IsSalutingInput())
        {
            StartSalute();
        }
        else
        {
            UpdateAnimator(moveInput.x);
        }
    }

    private void Move(Vector2 moveInput)
    {
        transform.Translate(moveInput * moveSpeed * Time.deltaTime);
    }

    private void FlipCharacter(float horizontalInput)
    {
        if (horizontalInput > 0 && !facingRight || horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private bool IsSalutingInput()
    {
        return saluteAction.action.triggered;
    }

    private void StartSalute()
    {
        isSaluting = true;
        animator.SetBool("IsSaluting", true);
    }

    private void StopSalute()
    {
        isSaluting = false;
        animator.SetBool("IsSaluting", false);
    }

    private void UpdateAnimator(float horizontalInput)
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }

}
