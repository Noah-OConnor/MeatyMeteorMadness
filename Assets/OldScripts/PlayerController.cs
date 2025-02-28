using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    private float horizontalInput;
    private Vector2 movementVector;
    private SpriteRenderer spriteRenderer;
    private Material initialMaterial;
    [SerializeField] private Material damageMaterial;
    public Animator animator;

    private Vector3 initialPosition;

    private void Start()
    {
        animator = GetComponent<Animator>();
        initialPosition = transform.position;
        movementVector = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (_GameManager.instance.lose) return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (horizontalInput != 0)
        {
            if (Mathf.Abs(transform.position.x + (horizontalInput * _GameManager.instance.GetMeteorSpeed()
                * Time.deltaTime)) > _GameManager.instance.GetHorizontalClamp())
            {
                movementVector.x = 0;
            }
            else
            {
                movementVector.x = horizontalInput;
            }

            transform.Translate(movementVector * _GameManager.instance.GetMeteorSpeed() * Time.deltaTime);
        }
        else
        {
            movementVector.x = 0;
        }

        if (_GameManager.instance.munch)
        {
            _GameManager.instance.munch = false;
            animator.Play("Player_Munch");
        }
        else if (_GameManager.instance.hurt)
        {
            _GameManager.instance.hurt = false;
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
        _GameManager.instance.SetInvincible(false);
    }

    public void ResetPosition()
    {
        ResetMaterial();
        transform.position = initialPosition;
        animator.Play("Player_Idle");
    }
}
