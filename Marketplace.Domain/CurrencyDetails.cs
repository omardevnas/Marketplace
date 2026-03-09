using Marketplace.Framework;

namespace Marketplace.Domain;

public class CurrencyDetails:Value<CurrencyDetails>
{
    public string CurrencyCode { get; set; }
    public bool InUse { get; set; }
    public int DecimalPlaces { get; set; }

    public static CurrencyDetails None = new CurrencyDetails() { InUse = false };


    public override bool Equals(CurrencyDetails? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return CurrencyCode == other.CurrencyCode;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(CurrencyCode);
    }
}