using NfeFiscal.Context;
using NfeFiscal.Models;
using NfeFiscal.Repository.Base;
using NfeFiscal.Repository.Interfaces;

namespace NfeFiscal.Repository;

public class ExportRepository : Repository<Export>, IExportRepository
{
    public ExportRepository(NfeContext context) : base(context)
    {
    }

    
}
