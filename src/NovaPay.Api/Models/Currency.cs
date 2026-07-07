namespace NovaPay.Api.Models;

public class Currency
{
    public string Code { get; set; } = string.Empty; // PK, e.g. "USD"
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public int DecimalPlaces { get; set; } = 2;
}
