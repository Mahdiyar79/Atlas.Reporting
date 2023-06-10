using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Atlas.Reporting.DAL.Domain.InternalSendingModel;

public partial class ChatGroup
{
    [Key]
    public Guid Id { get; set; }

    public string? GroupName { get; set; }

    public int BrandId { get; set; }

    public DateTime CreationDate { get; set; }

    [InverseProperty("ChatGroup")]
    public virtual ICollection<GroupChatMessage> GroupChatMessages { get; set; } = new List<GroupChatMessage>();

    [ForeignKey("GroupId")]
    [InverseProperty("Groups")]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
