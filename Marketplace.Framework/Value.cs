namespace Marketplace.Framework;

public abstract class Value<T>:IEquatable<T>
{

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Value<T>)obj);
    }

    public abstract bool Equals(T? other);
    public abstract int GetHashCode();
}