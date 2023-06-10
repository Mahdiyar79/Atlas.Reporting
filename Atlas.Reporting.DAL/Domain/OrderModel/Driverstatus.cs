﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("driverstatus", Schema = "delivering")]
public partial class Driverstatus
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [InverseProperty("DriverStatus")]
    public virtual ICollection<Driver> Drivers { get; set; } = new List<Driver>();
}
