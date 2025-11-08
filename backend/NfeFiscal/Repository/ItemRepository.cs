using Microsoft.EntityFrameworkCore;
using NfeFiscal.Context;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.Repository.Base;
using NfeFiscal.Repository.Interfaces;
using NfeFiscal.Responses;

namespace NfeFiscal.Repository;

public class ItemRepository : Repository<Item> , IItemRepository
{
    private ItemMapper _itemMapper;
    public ItemRepository(NfeContext context) : base(context)
    {
        _itemMapper = new ItemMapper();
    }

    public async Task<ItemResponse?> FindWithInclude(int id)
    {
        var item = await _context.Items
            .Include(i => i.Invoice)
            .FirstOrDefaultAsync(i => i.Id == id);

        if (item == null)
            return null;

        return await _itemMapper.EntityToResponse(item);
    }
}
