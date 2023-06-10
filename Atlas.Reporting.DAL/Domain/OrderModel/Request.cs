using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("requests", Schema = "ordering")]
public partial class Request
{
    [Key]
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime Time { get; set; }
}
