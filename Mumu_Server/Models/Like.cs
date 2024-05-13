using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Like
{
    [Key]
    [Column("LikeID")]
    public int LikeId { get; set; }

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [InverseProperty("Like")]
    public virtual ICollection<LikeDetail> LikeDetails { get; set; } = new List<LikeDetail>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Likes")]
    public virtual Project Project { get; set; } = null!;
}
