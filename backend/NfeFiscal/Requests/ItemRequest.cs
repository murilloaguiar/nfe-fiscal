using System.ComponentModel.DataAnnotations;

namespace NfeFiscal.Requests;

public class ItemRequest
{
    [Required]
    [StringLength(255)]
    public string Description { get; set; }
    [Required]
    [Range(0, double.MaxValue)]
    public double Value { get; set; }
    [Required]
    public int InvoiceId { get; set; }
}
