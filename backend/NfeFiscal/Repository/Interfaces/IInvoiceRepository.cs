using NfeFiscal.Models;
using NfeFiscal.Repository.Base;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository.Interfaces;

public interface IInvoiceRepository : IRepository<Invoice>
{
    Task<InvoiceResponse?> FindWithInclude(int id);
}
