using UnityEngine;

// DESIGN PATTERN - Facade
// Uses the InputManager to handle player movement instead of processing input directly.

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private float horizontalInput;
    private Vector2 movementVector;
    private SpriteRenderer spriteRenderer;
    private Material initialMaterial;
    [SerializeField] private Material damageMaterial;
    private Animator animator;

    private Vector3 initialPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        movementVector = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialMaterial = spriteRenderer.material;
    }

    public void HandleMovement(float horizontalInput)
    {
        if (GameManager.instance.lose) return;

        if (horizontalInput != 0)
        {
            if (Mathf.Abs(transform.position.x + (horizontalInput * GameManager.instance.GetMeteorSpeed()
                * Time.deltaTime)) > GameManager.instance.GetHorizontalClamp())
            {
                movementVector.x = 0;
            }
            else
            {
                movementVector.x = horizontalInput;
            }

            transform.Translate(movementVector * GameManager.instance.GetMeteorSpeed() * Time.deltaTime);
        }
        else
        {
            movementVector.x = 0;
        }

        UpdateAnimation(horizontalInput);
    }

    private void UpdateAnimation(float horizontalInput)
    {
        if (GameManager.instance.munch)
        {
            GameManager.instance.munch = false;
            animator.Play("Player_Munch");
        }
        else if (GameManager.instance.hurt)
        {
            GameManager.instance.hurt = false;
            animator.Play("Player_Hurt");
        }
        else
        {
            switch (horizontalInput)
            {
                case > 0:
                    animator.SetTrigger("Backward");
                    animator.ResetTrigger("Idle");
                    animator.ResetTrigger("Forward");
                    break;
                case < 0:
                    animator.SetTrigger("Forward");
                    animator.ResetTrigger("Idle");
                    animator.ResetTrigger("Backward");
                    break;
                case 0:
                    animator.SetTrigger("Idle");
                    animator.ResetTrigger("Forward");
                    animator.ResetTrigger("Backward");
                    break;
            }
        }
    }

    public void TakeDamage()
    {
        animator.Play("Player_Hurt");
        spriteRenderer.material = damageMaterial;
        CancelInvoke("ResetMaterial");
        Invoke("ResetMaterial", 1);
    }

    public void ResetMaterial()
    {
        spriteRenderer.material = initialMaterial;
        GameManager.instance.SetInvincible(false);
    }

    public void ResetPosition()
    {
        ResetMaterial();
        transform.position = initialPosition;
        animator.Play("Player_Idle");
    }
}
