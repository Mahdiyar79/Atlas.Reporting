using Atlas.Reporting.UI.Shared.DTOs;
using OfficeOpenXml;

namespace Atlas.Reporting.DAL.Services
{
    public interface IReportingService
    {
        public Task<List<ReportDto>> GetReportBy(ReportReqDto report);
        ExcelPackage CreateExcel(List<ReportDto> result, ExcelPackage package, MemoryStream stream);
    }
}
