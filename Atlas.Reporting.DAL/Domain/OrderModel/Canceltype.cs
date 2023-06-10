using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("canceltypes", Schema = "ordering")]
public partial class Canceltype
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("CancelType")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
