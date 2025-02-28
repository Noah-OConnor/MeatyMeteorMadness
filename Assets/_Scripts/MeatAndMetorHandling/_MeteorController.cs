using UnityEngine;

// DESIGN PATTERNS - Template Method, Prototype
// Implements the Template Method pattern by following a strict Update() sequence from FallingObject.
// Implements the Prototype pattern by allowing Meteor objects to be cloned instead of instantiated.
public class _MeteorController : FallingObject
{
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponentInChildren<Animator>();
    }

    protected override void HandleCollision()
    {
        if (contact)
        {
            if (!_GameManager.instance.GetInvincible())
            {
                _GameManager.instance.DamagePlayer();
                gameObject.SetActive(false);
            }
        }
    }
}
