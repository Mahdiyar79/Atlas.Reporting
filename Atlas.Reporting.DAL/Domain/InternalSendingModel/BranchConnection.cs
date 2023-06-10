using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

public partial class BranchConnection
{
    [Key]
    public int BranchId { get; set; }

    public string ConnectionId { get; set; } = null!;

    public DateTime LastConnection { get; set; }

    public bool IsConnected { get; set; }

    public int BrandId { get; set; }

    public DateTime LastPing { get; set; }

    [StringLength(100)]
    public string? UdId { get; set; }

    [Required]
    public bool? IsDisable { get; set; }
}
