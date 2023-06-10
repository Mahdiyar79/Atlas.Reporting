using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("branches", Schema = "ordering")]
public partial class Branch
{
    [Key]
    public int Id { get; set; }

    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required]
    public bool? IsActive { get; set; }

    public int WaitingOrders { get; set; }

    public int TotalDrivers { get; set; }

    public int PresentDrivers { get; set; }

    public int ReturningDrivers { get; set; }

    public int WaitingTime { get; set; }

    public int BrandId { get; set; }

    public int? DeliveryTime { get; set; }

    [InverseProperty("Branch")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();

    [InverseProperty("Branch")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
