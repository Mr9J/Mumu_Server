using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Admin
{
    [Key]
    public int AdminId { get; set; }

    public int MemberId { get; set; }

    [ForeignKey("MemberId")]
    [InverseProperty("Admins")]
    public virtual Member Member { get; set; } = null!;
}
