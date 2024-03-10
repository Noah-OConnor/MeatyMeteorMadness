using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorController : MonoBehaviour
{
    private float movementSpeed;
    private Vector2 movementVector;
    private bool contact = false;

    private void Start()
    {
        movementVector = Vector2.zero;
    }
    private void Update()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (!GameManager.instance.GetInvincible())
            {
                GameManager.instance.DamagePlayer();
                gameObject.SetActive(false);
            }
            else
            {
                contact = true;
            }
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contact = false;
        }
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    private void OnDestroy()
    {
        GameManager.instance.objectsToDelete.Remove(gameObject);
    }
}
