using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("deliverymethods", Schema = "ordering")]
public partial class Deliverymethod
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("DeliveryMethod")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
