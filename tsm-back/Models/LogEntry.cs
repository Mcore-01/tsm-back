using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace tsm_back.Models
{
    public class LogEntry
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public string EntityName { get; set; }
        public int EntityID { get; set; }
        public string Operation { get; set; }
        public DateTime Timestamp { get; set; }

        public override string ToString()
        {
            var result = string.Format("Id: {0}, UserID: {1}, EntityName: {2}, EntityID: {3}, Operation: {4}, Timestamp: {5}",
                Id, UserID, EntityName, EntityID, Operation, Timestamp.ToLongDateString());
            return result;
        }
    }
}
