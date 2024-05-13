using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class ActionDetail
{
    [Key]
    [Column("ActionDetailID")]
    public int ActionDetailId { get; set; }

    [Column("ActionID")]
    public int ActionId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [StringLength(50)]
    public string ActionName { get; set; } = null!;

    [ForeignKey("ActionId")]
    [InverseProperty("ActionDetails")]
    public virtual Action Action { get; set; } = null!;
}
