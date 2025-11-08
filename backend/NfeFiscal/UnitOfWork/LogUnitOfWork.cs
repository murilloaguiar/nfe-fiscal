using NfeFiscal.Context;
using NfeFiscal.Repository;
using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.UnitOfWork;
public class LogUnitOfWork : ILogUnitOfWork
{

    private EventLogRepository eventLogRepository;

    
    public IEventLogRepository EventLogRepository
    {
        get
        {
            if (eventLogRepository == null)
            {
                eventLogRepository = new EventLogRepository(context!);
            }
            return eventLogRepository;
        }
    }

    public LogContext context;

    public LogUnitOfWork(LogContext context)
    {
        this.context = context;
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

