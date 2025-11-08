using NfeFiscal.Models;
using NfeFiscal.Requests;
using NfeFiscal.Responses;

namespace NfeFiscal.Mappers;

public class ItemMapper
{

	public async Task<Item> RequestToEntity(ItemRequest request)
    {
        return new Item
        {
            Description = request.Description,
            Value = request.Value,
            InvoiceId = request.InvoiceId,
        };
    }

    public async Task<Item> UpdateRequestToEntity(ItemRequest request, Item invoice)
    {
        invoice.Description = request.Description;
        invoice.Value = request.Value;
        invoice.InvoiceId = request.InvoiceId;

        return invoice;
    }
	
    public async Task<ItemResponse> EntityToResponse(Item item)
    {
        return new ItemResponse
        {
            Id = item.Id,
            Description = item.Description,
            Value = item.Value,
            Invoice = MapInvoiceResponse(item.Invoice)

        };
    }

    private InvoiceResponse MapInvoiceResponse(Invoice invoice)
    {
        return new InvoiceResponse
        {
            Id = invoice.Id,
            Issuer = invoice.Issuer,
            IssueDate = invoice.IssueDate,
        };

    }
}
