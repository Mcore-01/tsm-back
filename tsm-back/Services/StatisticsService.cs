using ClosedXML.Excel;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using tsm_back.Repositories;
using tsm_back.ViewModels;

namespace tsm_back.Services
{
    public class StatisticsService
    {
        private readonly LogRepository _repository;
        public StatisticsService(LogRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<LogsStatisticDTO> GetStatisticLogs(int userID)
        {
            var logs = _repository.GetAllTodoLogsUser(userID);

            var logGroup = logs.GroupBy(p => p.Operation);

            var result = new List<LogsStatisticDTO>();
            foreach (var log in logGroup)
            {
                result.Add(new LogsStatisticDTO() { Name = log.Key, Value = log.Count() });
            }

            return result;
        }

        public byte[]? ExportExcelUserLogs(int userID)
        {
            var logs = _repository.GetAllTodoLogsUser(userID);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Logs");
                int currentIndex = 1;

                worksheet.Cell(currentIndex, 1).Value = "ID";
                worksheet.Cell(currentIndex, 2).Value = "TodoID";
                worksheet.Cell(currentIndex, 3).Value = "Операция";
                worksheet.Cell(currentIndex, 4).Value = "Время";

                foreach (var log in logs)
                {
                    currentIndex++;

                    worksheet.Cell(currentIndex, 1).Value = log.Id;
                    worksheet.Cell(currentIndex, 2).Value = log.EntityID;
                    worksheet.Cell(currentIndex, 3).Value = log.Operation;
                    worksheet.Cell(currentIndex, 4).Value = log.Timestamp;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return stream.ToArray();
                }
            }
        }
    }
}
