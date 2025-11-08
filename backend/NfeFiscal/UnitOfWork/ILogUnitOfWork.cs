using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.UnitOfWork;
public interface ILogUnitOfWork
{
    IEventLogRepository EventLogRepository { get; }

    Task Commit();
}

