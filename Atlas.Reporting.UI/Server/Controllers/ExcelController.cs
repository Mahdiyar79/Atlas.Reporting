using Atlas.Reporting.DAL.Services;
using Atlas.Reporting.UI.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;

[Route("api/[controller]")]
[ApiController]
public class ExcelController : ControllerBase
{
    private readonly IReportingService _reportingService;
    public ExcelController(IReportingService reportingService)
    {
        _reportingService = reportingService;
    }
    [HttpGet]
    public async Task<IActionResult> ExportExcel(string? vendorCode, DateTime startDate, DateTime endDate, bool? snappIsActive, int? brandId)
    {
        try
        {
            ReportReqDto report = new ReportReqDto()
            {
                VendorCode = vendorCode,
                StartDate = startDate,
                EndDate = endDate,
                SnappIsActive = snappIsActive,
                BrandId = brandId
            };

            List<ReportDto> reportDtos = await _reportingService.GetReportBy(report);
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var excel = _reportingService.CreateExcel(reportDtos, package, stream);

                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "report.xlsx";

                var newStream = new MemoryStream(excel.GetAsByteArray());
                return File(newStream, contentType, fileName);
            }
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

}