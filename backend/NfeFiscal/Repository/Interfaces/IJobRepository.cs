using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Repository.Base;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository.Interfaces;

public interface IJobRepository : IRepository<Job>
{
    Task<ICollection<JobResponse>> GetPending();
    Task<bool> VerifyIfInvoicesAlreadyQueued(ICollection<int> invoiceIds, ExportFormat format);

}
