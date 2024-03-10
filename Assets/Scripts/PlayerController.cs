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

    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        movementVector = new Vector2(0, 0);
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialMaterial = spriteRenderer.material;
    }

    private void Update()
    {
        if (GameManager.instance.lose) return;
        HandleMovement();
    }

    private void HandleMovement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
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
    }

    public void TakeDamage()
    {
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
        transform.position = initialPosition;
    }
}
