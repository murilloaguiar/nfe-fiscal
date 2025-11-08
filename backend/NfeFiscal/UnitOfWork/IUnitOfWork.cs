using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.UnitOfWork;

public interface IUnitOfWork
{
    IInvoiceRepository InvoiceRepository { get; }
    IItemRepository ItemRepository { get; }
    IJobRepository JobRepository { get; }
	IExportRepository ExportRepository { get; }
    Task Commit();
}
