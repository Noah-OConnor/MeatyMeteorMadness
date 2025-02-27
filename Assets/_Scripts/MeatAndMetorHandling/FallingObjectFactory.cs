using UnityEngine;

public abstract class FallingObjectFactory<T> where T : MonoBehaviour, IPrototype<T>
{
    protected T prototype;

    public FallingObjectFactory(T prototype)
    {
        this.prototype = prototype;
    }

    public abstract T Create();
}