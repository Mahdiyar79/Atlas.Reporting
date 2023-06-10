using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.SnappFoodModel;

public partial class Brand
{
    [Key]
    public int BrandId { get; set; }

    public string? BrandName { get; set; }

    public string? SnappClientId { get; set; }

    public string? SnappClientSecret { get; set; }

    public string? SnappUserName { get; set; }

    public string? SnappPassword { get; set; }

    [Required]
    public bool? IsEnable { get; set; }

    public bool? IsConnected { get; set; }
}
