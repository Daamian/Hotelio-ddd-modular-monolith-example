using Hotelio.Shared.Exception;

namespace Hotelio.Shared.Domain;

public record Price
{
    public double NetAmount { get; init; }
    public Currency Currency { get; init; }
    public int TaxRate { get; init; }

    private Price(double netAmount, Currency currency, int taxRate)
    {
        NetAmount = netAmount;
        Currency = currency;
        TaxRate = taxRate;
    }

    public static Price Create(double netAmount, Currency currency, int taxRate)
    {
        if (taxRate > 100)
        {
            throw new DomainException("Invalid tax rate percentage value");
        }
        
        return new Price(netAmount, currency, taxRate);
    }
    
    public static Price CreateDefault(double netAmount)
    {
        return new Price(netAmount, Currency.PLN, 23);
    }

    public double GetGrossAmount()
    {
        return NetAmount + NetAmount * (TaxRate/100);
    }
}