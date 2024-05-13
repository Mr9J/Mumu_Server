using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Action
{
    [Key]
    [Column("ActionID")]
    public int ActionId { get; set; }

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [InverseProperty("Action")]
    public virtual ICollection<ActionDetail> ActionDetails { get; set; } = new List<ActionDetail>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Actions")]
    public virtual Project Project { get; set; } = null!;
}
