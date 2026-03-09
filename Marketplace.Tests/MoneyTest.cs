using Marketplace.Domain;

namespace Marketplace.Tests;

public class MoneyTest
{
    
    private static readonly ICurrencyLookup CurrencyLookup=new FakeCurrencyLookup();

    [Fact]
    public void Two_Of_Same_Amount_Should_Be_Equal()
    {
        var firstAmount= Money.FromDecimal(5, "EUR", CurrencyLookup);
        var secondAmount= Money.FromDecimal(5, "EUR", CurrencyLookup);
        
        Assert.Equal(firstAmount, secondAmount);
    }
    
    [Fact]
    public void Two_Of_Same_Amount_ButDifferent_Currencies_Should_Not_Be_Equal()
    {
        var firstAmount= Money.FromDecimal(5, "EUR", CurrencyLookup);
        var secondAmount= Money.FromDecimal(5, "USD", CurrencyLookup);
        
        Assert.NotEqual(firstAmount, secondAmount);
    }
    [Fact]
    public void FromString_And_FromDecimal_Should_Be_Equal()
    {
        var firstAmount = Money.FromDecimal(5, "EUR",
            CurrencyLookup);
        var secondAmount = Money.FromString("5.00", "EUR",
            CurrencyLookup);
        Assert.Equal(firstAmount, secondAmount);
    }
    
    [Fact]
    public void Sum_Of_Money_Gives_Full_Amount()
    {
        var coin1 = Money.FromDecimal(1, "EUR", CurrencyLookup);
        var coin2 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var coin3 = Money.FromDecimal(2, "EUR", CurrencyLookup);
        var banknote = Money.FromDecimal(5, "EUR", CurrencyLookup);
        Assert.Equal(banknote, coin1 + coin2 + coin3);
    }
    [Fact]
    public void Unused_Currency_Should_Not_Be_Allowed()
    {
        Assert.Throws<ArgumentException>(() =>
            Money.FromDecimal(100, "DEM", CurrencyLookup)
        );
    }
    
    [Fact]
    public void Unknown_Currency_Should_Not_Be_Allowed()
    {
        Assert.Throws<ArgumentException>(() =>
            Money.FromDecimal(100, "WHAT?", CurrencyLookup)
        );
    }
    [Fact]
    public void Throw_When_Too_Many_Decimal_Places()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            Money.FromDecimal(100.123m, "EUR", CurrencyLookup)
        );
    }
    [Fact]
    public void Throws_On_Adding_Different_Currencies()
    {
        var firstAmount = Money.FromDecimal(5, "USD",
            CurrencyLookup);
        var secondAmount = Money.FromDecimal(5, "EUR",
            CurrencyLookup);
        Assert.Throws<CurrencyMismatchException>(() =>
            firstAmount + secondAmount
        );
    }
    [Fact]
    public void Throws_On_Substracting_Different_Currencies()
    {
        var firstAmount = Money.FromDecimal(5, "USD",
            CurrencyLookup);
        var secondAmount = Money.FromDecimal(5, "EUR",
            CurrencyLookup);
        Assert.Throws<CurrencyMismatchException>(() =>
            firstAmount - secondAmount
            );
    }
    
}