using Microsoft.AspNetCore.Mvc;
using NfeFiscal.Helpers;
using NfeFiscal.Models;
using NfeFiscal.UnitOfWork;

namespace NfeFiscal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExportsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    public ExportsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Export>>> Get(
        [FromQuery] bool paged = false,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 30)
    {
        try
        {
            var exports = await _unitOfWork.ExportRepository.GetAll();

            if (paged)
            {
                var pagedList = PagedList<Export>.Create(exports, pageNumber, pageSize);
                return Ok(pagedList);
            }

            return Ok(exports);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFile(int id)
    {
        try
        {
            var export = await _unitOfWork.ExportRepository.FindById(id);

            if (export == null)
                return NotFound();

			FileHelper.DeleteFile(export.Path);

            await _unitOfWork.ExportRepository.Delete(export);

            await _unitOfWork.Commit();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        return NoContent();
    }

    [HttpGet("Download/{id}")]
	public async Task<IActionResult> GetFile(int id)
	{

        var export = await _unitOfWork.ExportRepository.FindById(id);

        if (export == null)
            return NotFound();

        if (!System.IO.File.Exists(export.Path))
		{
			return NotFound();
		}

		var contentType = FileHelper.GetContentType(export.Format);

		return PhysicalFile(export.Path, contentType);
	}

}
