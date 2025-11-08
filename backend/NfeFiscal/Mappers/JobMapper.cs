using NfeFiscal.Models;
using NfeFiscal.Responses;

namespace NfeFiscal.Mappers;

public class JobMapper
{
    public ICollection<JobResponse> EntityListToResponseList(ICollection<Job> jobs)
    {
        return jobs.Select(a => EntityToResponse(a)).ToList();
    }
    public JobResponse EntityToResponse(Job job)
    {
        return new JobResponse
        {
            Id = job.Id,
            ExecutionTime = job.ExecutionTime,
            Status = job.Status,
            Invoice = MapInvoceResponse(job.Invoice),
            Format = job.Format
        };
    }

    private InvoiceResponse MapInvoceResponse(Invoice invoice)
    {
        return new InvoiceResponse
        {
            Id = invoice.Id,
            IssueDate = invoice.IssueDate,
            Items = invoice.Items.Select(i => MapItemResponse(i)).ToList()

        };
    }

    private ItemResponse MapItemResponse(Item item)
    {
        return new ItemResponse
        {
            Id = item.Id,
            Description = item.Description,
            Value = item.Value,

        };
    }
}
