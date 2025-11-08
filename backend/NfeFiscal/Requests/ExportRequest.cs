using System.ComponentModel.DataAnnotations;

namespace NfeFiscal.Requests;

public class ExportRequest
{
    [Required]
    public ICollection<int> InvoiceIds { get; set; }
    [Required]
    public string Format { get; set; }

}
