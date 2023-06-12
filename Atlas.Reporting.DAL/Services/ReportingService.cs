using Atlas.Reporting.DAL.Domain.InternalSendingModel;
using Atlas.Reporting.DAL.Domain.OrderModel;
using Atlas.Reporting.DAL.Domain.SnappFoodModel;
using Atlas.Reporting.UI.Shared.DTOs;
using Microsoft.EntityFrameworkCore;
using Atlas.Reporting.DAL.Domain.SnappFoodModel;

namespace Atlas.Reporting.DAL.Services
{
    public class ReportingService : IReportingService
    {
        private readonly InternalSendingContext _internalSendingContext;
        private readonly OrderContext _orderContext;
        private readonly SnappFoodContext _snappFoodContext;
        private ReportDto[]? _report = null;
        public ReportingService(InternalSendingContext internalSendingContext,
            OrderContext orderContext,
            SnappFoodContext snappFoodContext)
        {
            _orderContext = orderContext;
            _internalSendingContext = internalSendingContext;
            _snappFoodContext = snappFoodContext;
        }
        public async Task<List<ReportDto>> GetReportBy(ReportReqDto report)
        {
            List<Brand> brands = new List<Brand>();
            List<ReportDto> result = new List<ReportDto>();
            List<int> brandIds = new List<int>();

            if (report.StartDate == null && report.EndDate == null)
                throw new Exception("StartDate And EndDate Can not be null");

            //if(report.)
            if (report.BrandId != null || report.VendorCode != null)
            {
                //if (report.SnappIsActive != null && report.IsActive != null)
                //{
                //    brands = _snappFoodContext.Brands.Where(x => x.IsEnable == report.IsActive).ToList();
                //    brandIds.AddRange(brands.Select(a=>a.BrandId));
                //}
                //else
                //{
                //    if (report.IsActive != null)
                //    {
                //        brands = _snappFoodContext.Brands.Where(x => x.IsEnable == report.IsActive).ToList();
                //        brandIds.AddRange(brands.Select(a => a.BrandId)); brandIds.AddRange(_snappFoodContext.Brands.Where(x => x.IsEnable == report.IsActive).Select(x => x.BrandId).ToList());
                //    }
                //    else
                //    {
                //        brands = _snappFoodContext.Brands.Where(x => x.IsEnable == report.IsActive).ToList();
                //        brandIds.AddRange(brands.Select(a => a.BrandId));
                //    }
                //}
                Brand? brand;
                if (report.BrandId != null)
                    brand = _snappFoodContext.Brands.FirstOrDefault(a => a.BrandId == report.BrandId);
                else
                {
                    brand = _snappFoodContext.Brands.FirstOrDefault(a => a.SnappClientId == report.VendorCode);
                    report.BrandId = brand.BrandId;
                }
                var branchConnection = _internalSendingContext.BranchConnections.FirstOrDefault(a => a.BrandId == report.BrandId);

                result = await _orderContext.Orders
                    .Where(o => (o.OrderDate >= report.StartDate && o.OrderDate <= report.EndDate)
                                && o.BranchId == report.BrandId)
                    .GroupBy(o => new { o.BranchId, o.SourceTypeId })
                    .Select(g => new ReportDto
                    {
                        Raw = 1,
                        BrandId = g.Key.BranchId,
                        SnappFood = g.Key.SourceTypeId == 21 ? g.Sum(o => o.SourceTypeId) : 0,
                        Salon = g.Key.SourceTypeId == 2 ? g.Sum(o => o.SourceTypeId) : 0,
                        Total = g.Sum(o => o.SourceTypeId),
                        ClientId = brand.SnappClientId,
                        IsActive = brand.IsEnable,
                        LastPing = branchConnection.LastPing,
                        SnappIsActive = brand.IsEnable,
                        VendorName = brand.BrandName

                    })
                    .OrderBy(r => r.BrandId)
                    .ToListAsync();
            }

            return await Task.FromResult(result);
        }
    }
}