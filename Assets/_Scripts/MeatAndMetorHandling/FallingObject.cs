using UnityEngine;

// DESIGN PATTERNS - Template Method, Prototype
// Implements the Template Method pattern by defining a sealed Update() method that enforces 
// a sequence of function calls. Subclasses provide specific behavior by overriding protected hooks.
// Implements the Prototype pattern by providing a Clone() method to duplicate objects efficiently.
public abstract class FallingObject : MonoBehaviour, IPrototype<FallingObject>
{
    protected bool contact = false;
    protected float movementSpeed;
    protected Vector2 movementVector;

    protected virtual void Awake()
    {
        movementVector = Vector2.zero;
    }

    protected void OnEnable()
    {
        contact = false;
    }

    // Sealed method enforces a fixed update sequence
    protected void Update()
    {
        Move();
        HandleCollision();
        CheckOutOfBounds();
    }

    // Hook function for movement logic, can be overridden
    protected virtual void Move()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);
    }

    // Abstract function for collision handling, must be implemented by subclasses
    protected abstract void HandleCollision();

    // Common function that should never be changed by subclasses
    private void CheckOutOfBounds()
    {
        if (transform.position.y <= -6)
        {
            gameObject.SetActive(false); // Object Pooling: Disable instead of destroy
        }
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            contact = true;
        }
    }

    public virtual FallingObject Clone()
    {
        FallingObject clone = Instantiate(this);
        clone.SetMovementSpeed(this.movementSpeed);
        return clone;
    }
}
