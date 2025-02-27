public class MeteorFactory : FallingObjectFactory<_MeteorController>
{
    public MeteorFactory(_MeteorController prototype) : base(prototype) { }

    public override _MeteorController Create()
    {
        _MeteorController newMeteor = prototype.Clone();
        newMeteor.SetMovementSpeed(GameManager.instance.GetMeteorSpeed());
        return newMeteor;
    }
}
