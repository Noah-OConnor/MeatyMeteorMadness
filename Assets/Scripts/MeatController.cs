using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
    private float movementSpeed;
    private Vector2 movementVector;

    private void Start()
    {
        movementVector = Vector2.zero;
    }
    private void Update()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);

        if (transform.position.y <= -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EatMeat();
            Destroy(gameObject);
        }
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }
}
