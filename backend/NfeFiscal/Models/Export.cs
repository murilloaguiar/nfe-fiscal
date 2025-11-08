using NfeFiscal.Models.Enums;

namespace NfeFiscal.Models;

public class Export
{
    public int Id { get; set; }
    public string Path { get; set; }
    public string FileName { get; set; }
    public ExportFormat Format { get; set; }
    public int InvoiceId { get; set; }
    public DateTime Date { get; set; }

}
