using NfeFiscal.Models;
using NfeFiscal.Repository.Base;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository.Interfaces;

public interface IItemRepository : IRepository<Item>
{
    Task<ItemResponse?> FindWithInclude(int id);
}
