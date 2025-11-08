using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.Repository.Base;
using NfeFiscal.Repository.Interfaces;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository;

public class InvoiceRepository : Repository<Invoice> , IInvoiceRepository
{
    private InvoiceMapper _invoiceMapper;
    public InvoiceRepository(NfeContext context) : base(context)
    {
        _invoiceMapper = new InvoiceMapper();
    }

    public async Task<InvoiceResponse?> FindWithInclude(int id)
    {
        var invoice = await _context.Invoices
            .Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (invoice == null)
            return null;

        return await _invoiceMapper.EntityToResponse(invoice);
    }
}
