using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatController : MonoBehaviour
{
    private float movementSpeed;
    private Vector2 movementVector;
    
    ObjectPool objectPool;
    private void Start()
    {
        movementVector = Vector2.zero;
        objectPool = ObjectPool.Instance;
    }
    private void FixedUpdate()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EatMeat();
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(false);
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
