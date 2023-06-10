namespace Atlas.Reporting.UI.Shared.DTOs
{
    public class ReportReqDto
    {
        public DateTime? StartDate { get; set; }
        public int? BrandId { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? SnappIsActive { get; set; }
        public bool? IsActive { get; set; }
        public string?  VendorCode { get; set; }
    }
}
