// DESIGN PATTERN: Prototype
// Implements the Prototype pattern by defining a Clone() method, allowing objects to be duplicated.
// This optimizes object creation by copying existing instances instead of constructing new ones from scratch.

public interface IPrototype<T>
{
    T Clone();
}