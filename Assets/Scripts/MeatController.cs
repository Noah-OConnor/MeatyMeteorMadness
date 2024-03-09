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
    private void Update()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);

        if (transform.position.y <= -6)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EatMeat();
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
