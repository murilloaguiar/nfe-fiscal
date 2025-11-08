using NfeFiscal.Helpers;
using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Responses;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Services;

public class ExportService : IExportService
{
    private readonly IUnitOfWork _unitOfWork;
    private ILogger<ExportService> _logger;

    public ExportService(IUnitOfWork unitOfWork, ILogger<ExportService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }
    public async Task Start(JobResponse job)
    {
        try
        {

            if (job.Format == ExportFormat.json)
            {

                await JsonExport(job);

                return;
            }

			await TxtExport(job);

        }
        catch (Exception e)
        {
            string ex = e.InnerException != null ? e.InnerException.Message : e.Message;
            _logger.LogError(ex, "Não foi possível exportar o job {id}: " + ex, job.Id);
        }
    }

    private async Task JsonExport(JobResponse job)
    {
        
        string filePath = FileHelper.ExportInvoiceInJson(job.Invoice);
        await CreateExportInDB(job, filePath);
    }

    private async Task TxtExport(JobResponse job)
    {

        string filePath = FileHelper.ExportInvoiceInTxt(job.Invoice);
        await CreateExportInDB(job, filePath);
    }

    private async Task CreateExportInDB(JobResponse job, string path)
    {
        _logger.LogInformation($"Criando export no bd {job.Invoice.Id}");
        var export = new Export
        {
            Date = DateTime.UtcNow,
            InvoiceId = job.Invoice.Id,
            Path = path,
            FileName = $"invoice-{job.Invoice.Id}.{job.Format.ToString()}",
            Format = job.Format,

        };
        await _unitOfWork.ExportRepository.Add(export);
        
    }
}