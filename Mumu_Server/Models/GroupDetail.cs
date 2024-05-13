using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class GroupDetail
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("GroupID")]
    public int GroupId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [Column("AuthStatusID")]
    public int AuthStatusId { get; set; }

    [ForeignKey("AuthStatusId")]
    [InverseProperty("GroupDetails")]
    public virtual AuthStatus AuthStatus { get; set; } = null!;

    [ForeignKey("GroupId")]
    [InverseProperty("GroupDetails")]
    public virtual Group Group { get; set; } = null!;

    [ForeignKey("MemberId")]
    [InverseProperty("GroupDetails")]
    public virtual Member Member { get; set; } = null!;
}
