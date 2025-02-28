// DESIGN PATTERNS - Factory Method, Prototype
// Implements the Factory Method pattern by providing a structured way to create Meat objects.
// Uses the Prototype pattern by cloning an existing Meat prototype rather than creating new instances from scratch.

public class MeatFactory : FallingObjectFactory<MeatController>
{
    public MeatFactory(MeatController prototype) : base(prototype) { }

    public override MeatController Create()
    {
        MeatController newMeat = prototype.Clone() as MeatController;
        newMeat.SetMovementSpeed(GameManager.instance.GetMeteorSpeed() * 0.75f);
        return newMeat;
    }
}
