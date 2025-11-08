using Microsoft.AspNetCore.Mvc;
using NfeFiscal.Helpers;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.Requests;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ItemMapper _itemMapper;
    public ItemsController(IUnitOfWork unitOfWork, ItemMapper itemMapper)
    {
        _unitOfWork = unitOfWork;
        _itemMapper = itemMapper; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> Get(
        [FromQuery] bool paged = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 30)
    {
        try
        {
            var items = await _unitOfWork.ItemRepository.GetAll();

            if (paged)
            {
                var pagedList = PagedList<Item>.Create(items, pageNumber, pageSize);
                return Ok(pagedList);
            }

            return Ok(items);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<ActionResult> Post(ItemRequest request)
    {
        try
        {
            var invoice = await _itemMapper.RequestToEntity(request);

            if (!await InvoiceExists(invoice.InvoiceId))
                return BadRequest("Não foi possível encontrar a nota informada.");

            await _unitOfWork.ItemRepository.Add(invoice);
            await _unitOfWork.Commit();

            return Created();
        }
        catch (Exception e)
        { 
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, ItemRequest request)
    {
        try
        {
            var invoice = await _unitOfWork.ItemRepository.Find(m => m.Id == id);

            if (invoice == null)
                return NotFound();

            invoice = await _itemMapper.UpdateRequestToEntity(request, invoice);

            if (!await InvoiceExists(invoice.InvoiceId))
                return BadRequest("Não foi possível encontrar a nota informada.");

            await _unitOfWork.ItemRepository.Update(invoice);

            await _unitOfWork.Commit();

        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var invoice = await _unitOfWork.ItemRepository.Find(m => m.Id == id);

            if (invoice == null)
                return NotFound();

            await _unitOfWork.ItemRepository.Delete(invoice);
            await _unitOfWork.Commit();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Invoice>> Show(int id)
    {

        try
        {

            var invoice = await _unitOfWork.ItemRepository.FindWithInclude(id);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    private async Task<bool> InvoiceExists(int id)
    {
        return await _unitOfWork.InvoiceRepository.Any(v => v.Id == id);
    }

}
