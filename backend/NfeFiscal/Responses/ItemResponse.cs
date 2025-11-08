namespace NfeFiscal.Responses;

public class ItemResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public InvoiceResponse? Invoice { get; set; }

    public override string ToString()
    {
        // Use string interpolation para um formato conciso
        return $"{Id},{Description},{Value}";
    }
}
