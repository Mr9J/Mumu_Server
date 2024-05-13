using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class LikeDetail
{
    [Key]
    [Column("LikeDetailID")]
    public int LikeDetailId { get; set; }

    [Column("LikeID")]
    public int LikeId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [ForeignKey("LikeId")]
    [InverseProperty("LikeDetails")]
    public virtual Like Like { get; set; } = null!;

    [ForeignKey("MemberId")]
    [InverseProperty("LikeDetails")]
    public virtual Member Member { get; set; } = null!;
}
