using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

[Index("ReceiverId", Name = "IX_PrivateChatMessages_ReceiverId")]
[Index("SenderId", Name = "IX_PrivateChatMessages_SenderId")]
public partial class PrivateChatMessage
{
    [Key]
    public Guid Id { get; set; }

    [StringLength(200)]
    public string? Message { get; set; }

    public Guid SenderId { get; set; }

    public Guid ReceiverId { get; set; }

    public DateTime CreationDate { get; set; }

    [ForeignKey("ReceiverId")]
    [InverseProperty("PrivateChatMessageReceivers")]
    public virtual User Receiver { get; set; } = null!;

    [ForeignKey("SenderId")]
    [InverseProperty("PrivateChatMessageSenders")]
    public virtual User Sender { get; set; } = null!;
}
