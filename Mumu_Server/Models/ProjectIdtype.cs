using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("ProjectIDType")]
public partial class ProjectIdtype
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [Column("ProjectTypeID")]
    public int ProjectTypeId { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectIdtypes")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("ProjectTypeId")]
    [InverseProperty("ProjectIdtypes")]
    public virtual ProjectType ProjectType { get; set; } = null!;
}
