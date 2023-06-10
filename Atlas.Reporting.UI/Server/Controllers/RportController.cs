using Atlas.Reporting.DAL.Services;
using Atlas.Reporting.UI.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Atlas.Reporting.UI.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RportController : ControllerBase
    {
        private readonly IReportingService _reportingService;
        public RportController(IReportingService reportingService)
        {
            _reportingService = reportingService;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string? vendorCode , DateTime startDate, DateTime endDate , bool? snappIsActive,bool? isActive , int? brandId)
        {
            try
            {
                ReportReqDto report = new ReportReqDto()
                {
                    VendorCode = vendorCode,
                    StartDate = startDate,
                    EndDate = endDate,
                    SnappIsActive = snappIsActive,
                    IsActive = isActive,
                    BrandId = brandId
                };

                List<ReportDto> reportDtos = await _reportingService.GetReportBy(report);
                return Ok(reportDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            // ReportReqDto report = new ReportReqDto() 
            // {
            //     BrandId = brandId,
            //     StartDate = startDate,
            //     EndDate = endDate, 
            //     SnappIsActive = snappIsActive,
            //     IsActive = isAcyive,
            // };
            //return Ok( await  _reportingService.GetReportBy(report));


        }
    }
}
