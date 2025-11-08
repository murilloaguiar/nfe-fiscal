using NfeFiscal.Context;
using NfeFiscal.Repository;
using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private InvoiceRepository invoiceRepository;
    private ItemRepository itemRepository;
    private JobRepository jobRepository;
    private ExportRepository exportRepository;

    public NfeContext context;

    public UnitOfWork(NfeContext context)
    {
        this.context = context;
    }

    public UnitOfWork()
    {
        
    }

    public IInvoiceRepository InvoiceRepository
    {
        get
        {
            return invoiceRepository = invoiceRepository ?? new InvoiceRepository(context);
        }
    }

    public IItemRepository ItemRepository
    {
        get
        {
            return itemRepository = itemRepository ?? new ItemRepository(context);
        }
    }

    public IJobRepository JobRepository
    {
        get
        {
            return jobRepository = jobRepository ?? new JobRepository(context);
        }
    }

	public IExportRepository ExportRepository
    {
        get
        {
            return exportRepository = exportRepository ?? new ExportRepository(context);
        }
    
    }

    public async Task Commit()
    {
        await context!.SaveChangesAsync();
    }
    public void Dispose()
    {
        context!.Dispose();
    }

}
