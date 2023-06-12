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

            if (report.BrandId != null || report.VendorCode != null)
            {
                
                Brand? brand;
                if (report.BrandId != null)
                    brand = _snappFoodContext.Brands.FirstOrDefault(a => a.BrandId == report.BrandId);
                else
                {
                    brand = _snappFoodContext.Brands.FirstOrDefault(a => a.SnappClientId == report.VendorCode);
                    report.BrandId = brand.BrandId;
                }
                var branchConnection = _internalSendingContext.BranchConnections.FirstOrDefault(a => a.BrandId == report.BrandId);

                var res = await _orderContext.Orders
                     .Where(o => (o.OrderDate >= report.StartDate && o.OrderDate <= report.EndDate) && o.BranchId == report.BrandId).ToListAsync();
                var snappfood = res.Count(o => (o.SourceTypeId == 21));
                var salon = res.Count(o => (o.SourceTypeId == 2));
                var total = salon + snappfood;

                var fir = res.Select(o => new ReportDto
                {
                    Raw = rawCounter++,
                    BrandId = o.BranchId,
                    SnappFood = snappfood,
                    Salon = salon,
                    Total = total,
                    ClientId = brand.SnappClientId,
                    LastPing = branchConnection.LastPing,
                    SnappIsActive = brand.IsEnable,
                    VendorName = brand.BrandName

                }).FirstOrDefault();
                result.Add(fir);
            }
            else
            {

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
            return await Task.FromResult(result);
        }
    }
}