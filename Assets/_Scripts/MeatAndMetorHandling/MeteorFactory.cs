// DESIGN PATTERNS - Factory Method, Prototype
// Implements the Factory Method pattern by providing a structured way to create Meteor objects.
// Uses the Prototype pattern by cloning an existing Meteor prototype rather than creating new instances from scratch.

public class MeteorFactory : FallingObjectFactory<MeteorController>
{
    public MeteorFactory(MeteorController prototype) : base(prototype) { }

    public override MeteorController Create()
    {
        MeteorController newMeteor = prototype.Clone() as MeteorController;
        return newMeteor;
    }
}
