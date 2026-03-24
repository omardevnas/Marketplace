using Marketplace.Framework;

namespace Marketplace.Domain;

public class ClassifiedAdId:Value<ClassifiedAdId>
{
    private readonly Guid _value;

    public ClassifiedAdId(Guid value)
    {
        if(value==default)
            throw new ArgumentException("Value must be specified",nameof(value));
            
        _value = value;
    }

    public override bool Equals(ClassifiedAdId? other)
    {
        if(other is null) return false;
        if(ReferenceEquals(this, other)) return true;
        return this._value.Equals(other._value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    
    public static implicit operator Guid(ClassifiedAdId id) => id._value;
}