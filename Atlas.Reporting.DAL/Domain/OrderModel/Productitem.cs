using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[PrimaryKey("OrderItemId", "ProductId")]
[Table("productitems", Schema = "ordering")]
public partial class Productitem
{
    [Key]
    public Guid OrderItemId { get; set; }

    [Key]
    public int ProductId { get; set; }

    public double Units { get; set; }

    [StringLength(50)]
    public string ProductName { get; set; } = null!;

    public int UnitPrice { get; set; }

    [ForeignKey("OrderItemId")]
    [InverseProperty("Productitems")]
    public virtual OrderItem OrderItem { get; set; } = null!;
}
