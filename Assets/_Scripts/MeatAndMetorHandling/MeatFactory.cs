// DESIGN PATTERNS - Factory Method, Prototype
// Implements the Factory Method pattern by providing a structured way to create Meat objects.
// Uses the Prototype pattern by cloning an existing Meat prototype rather than creating new instances from scratch.

public class MeatFactory : FallingObjectFactory<_MeatController>
{
    public MeatFactory(_MeatController prototype) : base(prototype) { }

    public override _MeatController Create()
    {
        _MeatController newMeat = prototype.Clone() as _MeatController;
        newMeat.SetMovementSpeed(_GameManager.instance.GetMeteorSpeed() * 0.75f);
        return newMeat;
    }
}
