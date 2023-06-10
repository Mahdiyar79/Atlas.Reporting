using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("drivers", Schema = "delivering")]
[Index("BranchId", Name = "IX_drivers_BranchId")]
[Index("DriverStatusId", Name = "IX_drivers_DriverStatusId")]
public partial class Driver
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(50)]
    public string FullName { get; set; } = null!;

    [StringLength(11)]
    public string PhoneNo { get; set; } = null!;

    [StringLength(50)]
    public string? PersonelId { get; set; }

    [StringLength(2000)]
    public string? Invoices { get; set; }

    public int? BranchId { get; set; }

    public bool IsExternal { get; set; }

    [Required]
    public bool? IsEnabled { get; set; }

    public int DriverStatusId { get; set; }

    public string? ServiceBranches { get; set; }

    public DateTime StatusChangeTime { get; set; }

    [ForeignKey("BranchId")]
    [InverseProperty("Drivers")]
    public virtual Branch? Branch { get; set; }

    [ForeignKey("DriverStatusId")]
    [InverseProperty("Drivers")]
    public virtual Driverstatus DriverStatus { get; set; } = null!;

    [InverseProperty("Driver")]
    public virtual ICollection<Driverstatuslog> Driverstatuslogs { get; set; } = new List<Driverstatuslog>();
}
