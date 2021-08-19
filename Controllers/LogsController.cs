using Microsoft.AspNetCore.Mvc;
using my_book_store_v1.Data.Services;

namespace my_book_store_v1.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LogsController : ControllerBase
{
    #region DI _logsServices
    private readonly LogsServices _logsServices;

    public LogsController(LogsServices logsServices)
    {
        _logsServices = logsServices;
    }
    #endregion

    [HttpGet("get-all-logs-from-db")]
    public IActionResult GetAllLogs()
    {
        try
        {
            var _allLogs = _logsServices.GetAllLogs();
            return Ok(_allLogs);
        }
        catch (Exception ex)
        {

            return BadRequest(ex.Message+"This is come from GetAllLogs");
        }
    }



}
