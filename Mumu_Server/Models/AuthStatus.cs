using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("AuthStatus")]
public partial class AuthStatus
{
    [Key]
    [Column("AuthStatusID")]
    public int AuthStatusId { get; set; }

    [Column("AuthStatus")]
    [StringLength(50)]
    public string AuthStatus1 { get; set; } = null!;

    [InverseProperty("AuthStatus")]
    public virtual ICollection<GroupDetail> GroupDetails { get; set; } = new List<GroupDetail>();
}
