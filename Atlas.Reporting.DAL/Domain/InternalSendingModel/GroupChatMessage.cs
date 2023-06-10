using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

[Index("ChatGroupId", Name = "IX_GroupChatMessages_ChatGroupId")]
[Index("SenderId", Name = "IX_GroupChatMessages_SenderId")]
public partial class GroupChatMessage
{
    [Key]
    public Guid Id { get; set; }

    public string? Message { get; set; }

    public Guid SenderId { get; set; }

    public Guid GroupId { get; set; }

    public Guid? ChatGroupId { get; set; }

    public DateTime CreationDate { get; set; }

    [ForeignKey("ChatGroupId")]
    [InverseProperty("GroupChatMessages")]
    public virtual ChatGroup? ChatGroup { get; set; }

    [ForeignKey("SenderId")]
    [InverseProperty("GroupChatMessages")]
    public virtual User Sender { get; set; } = null!;
}
