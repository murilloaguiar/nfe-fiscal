using System.ComponentModel.DataAnnotations;

namespace NfeFiscal.Requests;

public class InvoiceRequest
{
    [Required]
    [StringLength(150)]
    public string Issuer { get; set; }
    public DateOnly IssueDate { get; set; }
}
