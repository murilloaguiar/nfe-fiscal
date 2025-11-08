using NfeFiscal.Models;

namespace NfeFiscal.Repository.Interfaces;

public interface IEventLogRepository
{
    Task<IEnumerable<EventLog>> GetAll();
}
