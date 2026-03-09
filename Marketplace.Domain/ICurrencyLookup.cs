namespace Marketplace.Domain;

public interface ICurrencyLookup
{
    public CurrencyDetails FindCurrency(string currencyCode);
}