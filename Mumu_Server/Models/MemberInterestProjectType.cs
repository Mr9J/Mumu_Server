using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("MemberInterestProjectType")]
public partial class MemberInterestProjectType
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ProjectTypeID")]
    public int ProjectTypeId { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [ForeignKey("MemberId")]
    [InverseProperty("MemberInterestProjectTypes")]
    public virtual Member Member { get; set; } = null!;

    [ForeignKey("ProjectTypeId")]
    [InverseProperty("MemberInterestProjectTypes")]
    public virtual ProjectType ProjectType { get; set; } = null!;
}
