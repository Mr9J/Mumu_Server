using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class ProjectType
{
    [Key]
    [Column("ProjectTypeID")]
    public int ProjectTypeId { get; set; }

    [StringLength(50)]
    public string ProjectTypeName { get; set; } = null!;

    [InverseProperty("ProjectType")]
    public virtual ICollection<MemberInterestProjectType> MemberInterestProjectTypes { get; set; } = new List<MemberInterestProjectType>();

    [InverseProperty("ProjectType")]
    public virtual ICollection<ProjectIdtype> ProjectIdtypes { get; set; } = new List<ProjectIdtype>();
}
