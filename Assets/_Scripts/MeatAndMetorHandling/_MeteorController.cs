using UnityEngine;

public class _MeteorController : MonoBehaviour, IPrototype<_MeteorController>
{
    private float movementSpeed;
    private Vector2 movementVector;
    private bool contact = false;
    private Animator animator;

    private void Awake()
    {
        movementVector = Vector2.zero;
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        movementVector.y = -movementSpeed * Time.deltaTime;
        transform.Translate(movementVector);

        if (transform.position.y <= -6)
        {
            gameObject.SetActive(false); // Instead of Destroying
        }

        if (contact && !GameManager.instance.GetInvincible())
        {
            GameManager.instance.DamagePlayer();
            gameObject.SetActive(false); // Instead of Destroying
        }
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
        animator.speed = speed * 0.33f;
    }

    public _MeteorController Clone()
    {
        _MeteorController clone = Instantiate(this);
        clone.SetMovementSpeed(this.movementSpeed);
        return clone;
    }
}
