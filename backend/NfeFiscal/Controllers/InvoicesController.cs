using Microsoft.AspNetCore.Mvc;
using NfeFiscal.Helpers;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.Models.Enums;
using NfeFiscal.Requests;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly InvoiceMapper _invoiceMapper;
    public InvoicesController(IUnitOfWork unitOfWork, InvoiceMapper invoiceMapper)
    {
        _unitOfWork = unitOfWork;
        _invoiceMapper = invoiceMapper; 
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Invoice>>> Get(
        [FromQuery] bool paged = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 30)
    {
        try
        {
            var invoices = await _unitOfWork.InvoiceRepository.GetAll();

            if (paged)
            {
                var pagedList = PagedList<Invoice>.Create(invoices, pageNumber, pageSize);
                return Ok(pagedList);
            }

            return Ok(invoices);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Post(InvoiceRequest request)
    {
        try
        {
            var invoice = await _invoiceMapper.RequestToEntity(request);
            await _unitOfWork.InvoiceRepository.Add(invoice);
            await _unitOfWork.Commit();

            return Created();
        }
        catch (Exception e)
        { 
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, InvoiceRequest request)
    {
        try
        {
            var invoice = await _unitOfWork.InvoiceRepository.Find(m => m.Id == id);

            if (invoice == null)
                return NotFound();

            invoice = await _invoiceMapper.UpdateRequestToEntity(request, invoice);

            await _unitOfWork.InvoiceRepository.Update(invoice);

            await _unitOfWork.Commit();

        }
        catch (Exception e)
        {
            return BadRequest();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var invoice = await _unitOfWork.InvoiceRepository.Find(m => m.Id == id);

            if (invoice == null)
                return NotFound();

            await _unitOfWork.InvoiceRepository.Delete(invoice);
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

            var invoice = await _unitOfWork.InvoiceRepository.FindWithInclude(id);

            if (invoice == null)
                return NotFound();

            return Ok(invoice);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("ExportInvoice")]
    public async Task<IActionResult> ExportInvoice(ExportRequest request)
    {
        try
        {

            var invoices = await _unitOfWork.InvoiceRepository.FindMany(j => request.InvoiceIds.Contains(j.Id));

            if (invoices.Count() == 0)
                return BadRequest();

            if (await InvoicesAlreadyQueued(request.InvoiceIds, Enum.Parse<ExportFormat>(request.Format)))
                return BadRequest();

            var jobs = await _invoiceMapper.InvoicesToJobList(invoices, Enum.Parse<ExportFormat>(request.Format));

            await _unitOfWork.JobRepository.AddRange(jobs);
            await _unitOfWork.Commit();
        }
        catch (Exception e)
        {

            return BadRequest();
        }
        return Ok();
    }

    private async Task<bool> InvoicesAlreadyQueued(ICollection<int> invoiceIds, ExportFormat format)
    {
        return await _unitOfWork.JobRepository.VerifyIfInvoicesAlreadyQueued(invoiceIds, format);
    }

}
