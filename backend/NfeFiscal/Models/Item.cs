namespace NfeFiscal.Models;

public class Item
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Value { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
