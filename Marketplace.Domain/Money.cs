using Marketplace.Framework;

namespace Marketplace.Domain;

public class Money:Value<Money>
{
    public decimal Amount {get;}
    public string CurrencyCode { get; private set; }

    private const string DefaultCurrency = "EUR";
    
    protected Money(decimal amount,string currency="EUR" )
    {
        
        if (decimal.Round(amount, 2) != amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a valid decimal");
        }
        Amount = amount;
        CurrencyCode = currency;
    }


    public static Money FromDecimal(decimal amount,string currency = DefaultCurrency)
    {
        return new Money(amount,currency);
    }

    public static Money FromString(string amount,string currency = DefaultCurrency)
    {
        return new Money(decimal.Parse(amount),currency);
    }

    public Money Add(Money other)
    {
        if(CurrencyCode != other.CurrencyCode)
            throw new CurrencyMismatchException($"Cannot add money in different currencies: {CurrencyCode} and {other.CurrencyCode}");
        return new Money(Amount + other.Amount);
    }

    public Money Subtract(Money other)
    {
        if(CurrencyCode != other.CurrencyCode)
            throw new CurrencyMismatchException($"Cannot subtract money in different currencies: {CurrencyCode} and {other.CurrencyCode}");

        return new Money(Amount - other.Amount);
    }

    public static Money operator +(Money left, Money right)=> left.Add(right);
    
    public static Money operator -(Money left, Money right)=> left.Subtract(right);
    
    public override bool Equals(Money? other)
    {
        if(other is null) return false;
        if(ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount);
    }
}

public class CurrencyMismatchException : Exception
{
    public CurrencyMismatchException(string message) :
        base(message)
    {
    }
}
