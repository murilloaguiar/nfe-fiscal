using NfeFiscal.Models.Enums;

namespace NfeFiscal.Responses;

public class JobResponse
{
    public int Id { get; set; }
    public JobStatus Status { get; set; }
    public ExportFormat Format { get; set; }
    public double ExecutionTime { get; set; }
    public int InvoiceId { get; set; }
    public InvoiceResponse Invoice { get; set; }
}
