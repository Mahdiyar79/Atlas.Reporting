using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("orderItems", Schema = "ordering")]
[Index("OrderId", Name = "IX_orderItems_OrderId")]
public partial class OrderItem
{
    [Key]
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public int BaseProductId { get; set; }

    [Column("VAT")]
    public int Vat { get; set; }

    [StringLength(100)]
    public string? Description { get; set; }

    public int Discount { get; set; }

    [StringLength(50)]
    public string ProductName { get; set; } = null!;

    public int UnitPrice { get; set; }

    public double Units { get; set; }

    public int PackagingCost { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;

    [InverseProperty("OrderItem")]
    public virtual ICollection<Productitem> Productitems { get; set; } = new List<Productitem>();
}
