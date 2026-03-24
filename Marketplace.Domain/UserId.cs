using Marketplace.Framework;

namespace Marketplace.Domain;

public class UserId:Value<UserId>
{
    private readonly Guid _value;

    public UserId(Guid value)
    {
        if(value==default)
            throw new ArgumentException("Value must be specified",nameof(value));
        _value = value;
    }

    public override bool Equals(UserId? other)
    {
        if(other is null) return false;
        if(ReferenceEquals(this,other))return true;
        return _value.Equals(other._value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_value);
    }
    public static implicit operator Guid(UserId id) => id._value;
}