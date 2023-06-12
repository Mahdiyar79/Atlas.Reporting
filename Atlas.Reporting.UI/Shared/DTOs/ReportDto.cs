namespace Atlas.Reporting.UI.Shared.DTOs
{
    public class ReportDto
    {
        public ReportDto()
        {
            Raw++;
        }
        public int Raw { get; set; } = 0;
        public int BrandId { get; set; }
        public string? ClientId { get; set; }
        public DateTime LastPing { get; set; }
        public string? VendorName { get; set; }
        public int Total { get; set; }
        public int SnappFood { get; set; }
        public int Salon { get; set; }
        public bool? SnappIsActive { get; set; }
    }
}
