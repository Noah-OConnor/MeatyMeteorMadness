using UnityEngine;

public class _MeatController : MonoBehaviour, IPrototype<_MeatController>
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
            gameObject.SetActive(false); // Instead of Destroying
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.EatMeat();
            gameObject.SetActive(false); // Instead of Destroying
        }
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public _MeatController Clone()
    {
        _MeatController clone = Instantiate(this);
        clone.SetMovementSpeed(this.movementSpeed);
        return clone;
    }
}
