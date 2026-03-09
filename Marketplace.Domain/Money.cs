using Marketplace.Framework;

namespace Marketplace.Domain;

public class Money:Value<Money>
{
    public decimal Amount {get;}
    public CurrencyDetails Currency { get; private set; }
    
    protected Money(decimal amount,string currency,ICurrencyLookup currencyLookup)
    {
        if (string.IsNullOrWhiteSpace(currency))
        {
            throw new ArgumentException("Currency must be specified", nameof(currency));
        }
        var currencyCode= currencyLookup.FindCurrency(currency);
        if(!currencyCode.InUse)
            throw new ArgumentException($"Currency {currency} is not in use", nameof(currency));
        if (decimal.Round(amount, currencyCode.DecimalPlaces) != amount)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be a valid decimal");
        }
        Amount = amount;
        Currency = currencyCode;
    }
    
    
    private Money(decimal amount, CurrencyDetails currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money FromDecimal(decimal amount,string currency, ICurrencyLookup currencyLookup)
    {
        return new Money(amount,currency,currencyLookup);
    }

    public static Money FromString(string amount,string currency, ICurrencyLookup currencyLookup)
    {
        return new Money(decimal.Parse(amount),currency,currencyLookup);
    }

    public Money Add(Money other)
    {
        if(Currency != other.Currency)
            throw new CurrencyMismatchException($"Cannot add money in different currencies: {Currency.CurrencyCode} and {other.Currency.CurrencyCode}");
        return new Money(Amount + other.Amount,Currency);
    }

    public Money Subtract(Money other)
    {
        if(Currency != other.Currency)
            throw new CurrencyMismatchException($"Cannot subtract money in different currencies: {Currency.CurrencyCode} and {other.Currency.CurrencyCode}");

        return new Money(Amount - other.Amount,Currency);
    }

    public static Money operator +(Money left, Money right)=> left.Add(right);
    
    public static Money operator -(Money left, Money right)=> left.Subtract(right);
    
    public override bool Equals(Money? other)
    {
        if(other is null) return false;
        if(ReferenceEquals(this, other)) return true;
        return Amount == other.Amount;
    }

    public override string ToString()
    {
        return $"{Currency.CurrencyCode} {Amount}";
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Amount);
    }
}