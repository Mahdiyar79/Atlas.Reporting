using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.OrderModel;

[Table("IntegrationEventLog")]
public partial class IntegrationEventLog
{
    [Key]
    public Guid EventId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public string EventTypeName { get; set; } = null!;

    public int State { get; set; }

    public int TimesSent { get; set; }

    public string? TransactionId { get; set; }
}
