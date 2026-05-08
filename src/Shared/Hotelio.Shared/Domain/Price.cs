using Hotelio.Shared.Exception;

namespace Hotelio.Shared.Domain;

public record Price
{
    public decimal NetAmount { get; init; }
    public Currency Currency { get; init; }
    public int TaxRate { get; init; }

    private Price(decimal netAmount, Currency currency, int taxRate)
    {
        NetAmount = netAmount;
        Currency = currency;
        TaxRate = taxRate;
    }

    public static Price Create(decimal netAmount, Currency currency, int taxRate)
    {
        if (taxRate > 100)
        {
            throw new DomainException("Invalid tax rate percentage value");
        }
        
        return new Price(netAmount, currency, taxRate);
    }
    
    public static Price CreateDefault(decimal netAmount)
    {
        return new Price(netAmount, Currency.PLN, 23);
    }

    public decimal GetGrossAmount()
    {
        return NetAmount * (1 + TaxRate / 100m);
    }
}