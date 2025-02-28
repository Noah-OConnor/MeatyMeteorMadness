// DESIGN PATTERN: Factory Method
// Implements the Factory Method pattern by defining an abstract factory for creating FallingObject instances.
// Subclasses (MeatFactory, MeteorFactory) implement specific object creation details, promoting flexibility and maintainability.

public abstract class FallingObjectFactory<T> where T : FallingObject
{
    protected T prototype;

    public FallingObjectFactory(T prototype)
    {
        this.prototype = prototype;
    }

    public abstract T Create();
}
