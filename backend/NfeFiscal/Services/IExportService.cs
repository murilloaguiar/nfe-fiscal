using NfeFiscal.Models;
using NfeFiscal.Responses;

namespace NfeFiscal.Services;

public interface IExportService
{
    Task Start(JobResponse job);
}