using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NfeFiscal.Helpers;
using NfeFiscal.Models;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventLogsController : ControllerBase
{
    private readonly ILogUnitOfWork _unitOfWork;

    public EventLogsController(ILogUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EventLog>>> Get(
        [FromQuery] string? filter,
        [FromQuery] bool paged = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 30,
        [FromQuery] string? sorted = null)
    {
        try
        {
            var logs = await _unitOfWork.EventLogRepository.GetAll();

            if (paged)
            {
                var pagedList = PagedList<EventLog>.Create(logs, pageNumber, pageSize);
                return Ok(pagedList);
            }

            return Ok(logs);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }



}
