namespace NfeFiscal.Responses;

public class InvoiceResponse
{
    public int Id { get; set; }
    public string Issuer { get; set; }
    public DateOnly IssueDate { get; set; }
    public ICollection<ItemResponse>? Items { get; set; }

    public override string ToString()
    {
        return $"{Id},{Issuer},{IssueDate}";
    }
}
