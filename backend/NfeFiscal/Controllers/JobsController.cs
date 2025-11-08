using Microsoft.AspNetCore.Mvc;
using NfeFiscal.Helpers;
using NfeFiscal.Mappers;
using NfeFiscal.Models;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class JobsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public JobsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> Get(
        [FromQuery] bool paged = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 30)
    {
        try
        {
            var items = await _unitOfWork.JobRepository.GetAll();

            if (paged)
            {
                var pagedList = PagedList<Job>.Create(items, pageNumber, pageSize);
                return Ok(pagedList);
            }

            return Ok(items);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

}
