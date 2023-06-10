using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("driverstatuslogs", Schema = "delivering")]
[Index("DriverId", Name = "IX_driverstatuslogs_DriverId")]
public partial class Driverstatuslog
{
    [Key]
    public Guid Id { get; set; }

    public Guid DriverId { get; set; }

    public int? BranchId { get; set; }

    public int DriverStatusId { get; set; }

    public DateTime StatusTime { get; set; }

    [ForeignKey("DriverId")]
    [InverseProperty("Driverstatuslogs")]
    public virtual Driver Driver { get; set; } = null!;
}
