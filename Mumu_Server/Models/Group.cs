using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Group
{
    [Key]
    [Column("GroupID")]
    public int GroupId { get; set; }

    [StringLength(50)]
    public string GroupName { get; set; } = null!;

    [InverseProperty("Group")]
    public virtual ICollection<GroupDetail> GroupDetails { get; set; } = new List<GroupDetail>();

    [InverseProperty("Group")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
