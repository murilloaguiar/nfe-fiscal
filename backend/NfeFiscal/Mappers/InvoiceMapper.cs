using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Requests;
using NfeFiscal.Responses;

namespace NfeFiscal.Mappers;

public class InvoiceMapper
{
    public async Task<Invoice> RequestToEntity(InvoiceRequest request)
    {
        return new Invoice
        {
            IssueDate = request.IssueDate,
            Issuer = request.Issuer,
        };
    }

    public async Task<Invoice> UpdateRequestToEntity(InvoiceRequest request, Invoice invoice)
    {
        invoice.IssueDate = request.IssueDate;
        invoice.Issuer = request.Issuer;

        return invoice;
    }

    public async Task<InvoiceResponse> EntityToResponse(Invoice invoice)
    {
        return new InvoiceResponse
        {
            Id = invoice.Id,
            IssueDate = invoice.IssueDate,
            Issuer = invoice.Issuer,
            Items = invoice.Items.Select(i => MapItemResponse(i)).ToList()
        };
    }

    public async Task<IEnumerable<Job>> InvoicesToJobList(IEnumerable<Invoice> invoices, ExportFormat format)
    {
        var jobList = new List<Job>();
        foreach (var invoice in invoices)
        {
            var job = new Job
            {
                InvoiceId = invoice.Id,
                Format = format,
                Status = JobStatus.pending

            };
            jobList.Add(job);
        }
        return jobList;
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
