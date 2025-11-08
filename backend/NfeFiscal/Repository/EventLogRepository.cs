using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using NfeFiscal.Models;
using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.Repository;

public class EventLogRepository : IEventLogRepository
{
    private readonly LogContext _context;
    public EventLogRepository(LogContext contex) 
    {
        _context = contex;
    }
    public async Task<IEnumerable<EventLog>> GetAll()
    {
        return _context.EventLogs.AsNoTracking().ToList();
    }
}
