using Unity.VisualScripting;

public class MeatFactory : FallingObjectFactory<_MeatController>
{
    public MeatFactory(_MeatController prototype) : base(prototype) { }

    public override _MeatController Create()
    {
        _MeatController newMeat = prototype.Clone();
        newMeat.SetMovementSpeed(GameManager.instance.GetMeteorSpeed() * 0.75f);
        return newMeat;
    }
}
