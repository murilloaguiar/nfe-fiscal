using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Repository.Base;
using NfeFiscal.Repository.Interfaces;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository;

public class JobRepository : Repository<Job>, IJobRepository
{
    private readonly JobMapper _jobMapper;
    public JobRepository(NfeContext context) : base(context)
    {
        _jobMapper = new JobMapper();
    }

    public async Task<ICollection<JobResponse>> GetPending()
    {
        var jobs = await _context.Jobs
            .Include(j => j.Invoice).ThenInclude(i => i.Items)
            .Where(j => j.Status == JobStatus.pending)
            .ToListAsync();

        if (jobs.Count == 0)
            return new List<JobResponse>();

        return _jobMapper.EntityListToResponseList(jobs);
    }

    public async Task<bool> VerifyIfInvoicesAlreadyQueued(ICollection<int> invoiceIds, ExportFormat format)
    {
        return await _context.Jobs
            .Where(j => invoiceIds.Contains(j.InvoiceId))
            .Where(j => j.Status == JobStatus.pending)
            .Where(j => j.Format == format)
            .AnyAsync();
    }
}
