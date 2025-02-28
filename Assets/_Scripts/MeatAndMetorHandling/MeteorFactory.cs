// DESIGN PATTERNS - Factory Method, Prototype
// Implements the Factory Method pattern by providing a structured way to create Meteor objects.
// Uses the Prototype pattern by cloning an existing Meteor prototype rather than creating new instances from scratch.

public class MeteorFactory : FallingObjectFactory<_MeteorController>
{
    public MeteorFactory(_MeteorController prototype) : base(prototype) { }

    public override _MeteorController Create()
    {
        _MeteorController newMeteor = prototype.Clone() as _MeteorController;
        newMeteor.SetMovementSpeed(_GameManager.instance.GetMeteorSpeed());
        return newMeteor;
    }
}
