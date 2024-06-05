using tsm_back.Models;
using tsm_back.Data;

namespace tsm_back.Repositories
{
    public class LogRepository
    {
        private readonly TSMContext _context;
        public LogRepository(TSMContext context)
        {
            _context = context;
        }

        public IEnumerable<LogEntry> GetAllTodoLogsUser(int userID)
        {
            return _context.Logs
                 .Where(p => p.UserID == userID);
        }
    }
}
