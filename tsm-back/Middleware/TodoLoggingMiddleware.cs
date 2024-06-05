using Microsoft.EntityFrameworkCore;
using tsm_back.Data;
using tsm_back.Models;
using tsm_back.Repositories;

namespace tsm_back.Middleware
{
    public class TodoLoggingMiddleware
    {
        private readonly RequestDelegate _next;
 


        public TodoLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            
            LogChanges(context);
        }

        private void LogChanges(HttpContext context)
        {
            if (context.Response.Headers["entity"] == typeof(Todo).Name)
            {  
                var logEntry = new LogEntry()
                {
                    UserID = Convert.ToInt32(context.Request.Headers["userID"]),
                    EntityName = context.Response.Headers["entity"],
                    EntityID = Convert.ToInt32(context.Response.Headers["entityID"]),
                    Operation = context.Response.Headers["operation"],
                    Timestamp = DateTime.Now.ToUniversalTime()
                };
                var _context = context.RequestServices.GetRequiredService<TSMContext>();
                _context.Logs.Add(logEntry);
                _context.SaveChanges();
                Console.WriteLine(logEntry.ToString());
                
            }
            
        }
    }
}
