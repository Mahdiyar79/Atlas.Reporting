using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

public partial class User
{
    [Key]
    public Guid UserId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? CellPhoneNo { get; set; }

    public string? Description { get; set; }

    public int BrandId { get; set; }

    [InverseProperty("Sender")]
    public virtual ICollection<GroupChatMessage> GroupChatMessages { get; set; } = new List<GroupChatMessage>();

    [InverseProperty("Receiver")]
    public virtual ICollection<PrivateChatMessage> PrivateChatMessageReceivers { get; set; } = new List<PrivateChatMessage>();

    [InverseProperty("Sender")]
    public virtual ICollection<PrivateChatMessage> PrivateChatMessageSenders { get; set; } = new List<PrivateChatMessage>();

    [ForeignKey("UserId")]
    [InverseProperty("Users")]
    public virtual ICollection<ChatGroup> Groups { get; set; } = new List<ChatGroup>();
}
