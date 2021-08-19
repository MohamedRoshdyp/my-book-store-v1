
using my_book_store_v1.Data.Models;

namespace my_book_store_v1.Data.Services;
public class LogsServices
{
    #region DI _context
    private readonly AppDbContext _context;

    public LogsServices(AppDbContext context)
    {
        _context = context;
    }
    #endregion


    //Get All Logs
    public List<Log> GetAllLogs() => _context.Logs.ToList();

}
