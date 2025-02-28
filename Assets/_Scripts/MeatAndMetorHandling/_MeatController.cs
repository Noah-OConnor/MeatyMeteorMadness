using UnityEngine;

// DESIGN PATTERNS - Template Method, Prototype
// Implements the Template Method pattern by following a strict Update() sequence from FallingObject.
// Implements the Prototype pattern by allowing Meat objects to be cloned instead of instantiated.

public class _MeatController : FallingObject
{
    protected override void HandleCollision()
    {
        if (contact)
        {
            _GameManager.instance.EatMeat();
            gameObject.SetActive(false);
        }
    }
}
