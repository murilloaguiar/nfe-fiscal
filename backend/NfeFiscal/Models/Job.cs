using NfeFiscal.Models.Enums;

namespace NfeFiscal.Models;

public class Job
{
    public int Id { get; set; }
    public JobStatus Status { get; set; }
    public ExportFormat Format { get; set; }
    public double ExecutionTime { get; set; }
    public int InvoiceId { get; set; }
    public Invoice Invoice { get; set; }
}
