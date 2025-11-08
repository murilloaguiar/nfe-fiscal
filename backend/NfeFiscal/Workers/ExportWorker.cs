using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Services;
using NfeFiscal.UnitOfWork;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NfeFiscal.Workers;

public class ExportWorker : BackgroundService
{
    private readonly IServiceProvider _provider;
    private readonly IConfiguration _config;
    private readonly ILogger<ExportWorker> _logger;

    public ExportWorker(IServiceProvider provider, IConfiguration config, ILogger<ExportWorker> logger)
    {
        _provider = provider;
        _config = config;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        TimeSpan interval = TimeSpan.FromSeconds(10);
		
        using PeriodicTimer timer = new PeriodicTimer(interval);

        while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
        {
            await RunInBackground();
        }
    }

    private async Task RunInBackground()
    {
        using var scope = _provider.CreateScope();

        var _unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var pendingJobs = await _unitOfWork.JobRepository.GetPending();

        if (pendingJobs.Count == 0) return; 

        var _exportService = scope.ServiceProvider.GetRequiredService<IExportService>();
        Stopwatch stopwatch = new Stopwatch();

        foreach (var job in pendingJobs)
        {
            _logger.LogInformation($"Executando job {job.Id}");
            await JobProcessing(_unitOfWork, job.Id);

            stopwatch.Start();
            await _exportService.Start(job);
            stopwatch.Stop();

            double ts = stopwatch.Elapsed.TotalSeconds;
            await JobDone(_unitOfWork, job.Id, ts);
        }
        
    }

    private async Task JobProcessing(IUnitOfWork _unitOfWork, int jobId)
    {
        var job = await _unitOfWork.JobRepository.FindById(jobId);
        job.Status = Enum.Parse<JobStatus>("processing");

        await _unitOfWork.JobRepository.Update(job);
        await _unitOfWork.Commit();
    }

    private async Task JobDone(IUnitOfWork _unitOfWork, int jobId, double ts = 0)
    {
        var job = await _unitOfWork.JobRepository.FindById(jobId);
        job.Status = Enum.Parse<JobStatus>("done");
        job.ExecutionTime = ts;
        await _unitOfWork.JobRepository.Update(job);
        await _unitOfWork.Commit();
    }
}
