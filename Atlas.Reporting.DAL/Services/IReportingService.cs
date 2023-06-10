using Atlas.Reporting.UI.Shared.DTOs;

namespace Atlas.Reporting.DAL.Services
{
    public interface IReportingService
    {
        public Task<List<ReportDto>> GetReportBy(ReportReqDto report);
    }
}
