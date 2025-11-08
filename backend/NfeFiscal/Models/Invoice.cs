namespace NfeFiscal.Models;

public class Invoice
{
    public int Id { get; set; }
    public string Issuer { get; set; }
    public DateOnly IssueDate { get; set; }
    public IEnumerable<Item> Items { get; set; }
}
