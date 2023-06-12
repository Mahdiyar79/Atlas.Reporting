using Atlas.Reporting.DAL.Domain.InternalSendingModel;
using Atlas.Reporting.DAL.Domain.OrderModel;
using Atlas.Reporting.DAL.Domain.SnappFoodModel;
using Atlas.Reporting.UI.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Services
{
    public class ReportingService : IReportingService
    {
        private readonly InternalSendingContext _internalSendingContext;
        private readonly OrderContext _orderContext;
        private readonly SnappFoodContext _snappFoodContext;
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
            List<ReportDto> result = new List<ReportDto>();
            List<int> brandIds = new List<int>();
            int rawCounter = 1;

            if (report.StartDate == null && report.EndDate == null)
                throw new Exception("StartDate And EndDate Can not be null");

            var ordersQuery = _orderContext.Orders
        .Where(o => o.OrderDate >= report.StartDate && o.OrderDate <= report.EndDate);

            result = await _orderContext.Branches
                .GroupJoin(
                    ordersQuery,
                    b => b.BrandId,
                    o => o.BranchId,
                    (b, o) => new { Branch = b, Orders = o }
                )
                .Select(x => new ReportDto
                {
                    BrandId = x.Branch.BrandId,
                    SnappFood = x.Orders.Count(o => o.SourceTypeId == 21),
                    Salon = x.Orders.Count(o => o.SourceTypeId == 2),
                    Total = x.Orders.Count(o => o.SourceTypeId == 21) + x.Orders.Count(o => o.SourceTypeId == 2),
                })
                .OrderBy(x => x.BrandId)
                .ToListAsync();

            brandIds = result.Select(a => a.BrandId).ToList();
            List<Brand> snappBrands = await _snappFoodContext.Brands.Where(b => (brandIds.Contains(b.BrandId))).ToListAsync();

            if (report.SnappIsActive != null)
                snappBrands = await _snappFoodContext.Brands.Where(b => (brandIds.Contains(b.BrandId)) && b.IsEnable == report.SnappIsActive).ToListAsync();
            else
                await _snappFoodContext.Brands.Where(b => (brandIds.Contains(b.BrandId))).ToListAsync();

            if ((report.BrandId != null) || (!string.IsNullOrEmpty(report.VendorCode)))
            {
                if (report.BrandId != null)
                {
                    result = result.Where(r => r.BrandId == report.BrandId).ToList();
                }
                else if (!string.IsNullOrEmpty(report.VendorCode))
                {
                    result = result.Where(r => snappBrands.Any(b => b.BrandId == r.BrandId && b.SnappClientId == report.VendorCode)).ToList();
                }
            }

            result = result.Join(snappBrands,
                r => r.BrandId,
                s => s.BrandId,
                (r, s) =>
                  {
                      r.Raw = rawCounter++;
                      r.VendorName = s.BrandName;
                      r.SnappIsActive = s.IsEnable;
                      r.ClientId = s.SnappClientId;
                      return r;
                  }).ToList();

            var branchBrands = await _internalSendingContext.BranchConnections.Where(b => brandIds.Contains(b.BrandId)).ToListAsync();
            result = result.Join(branchBrands,
                r => r.BrandId,
                b => b.BrandId,
                (r, b) =>
                  {
                      r.LastPing = b.LastPing;
                      return r;
                  }).ToList();

            return result;
        }
    }
}