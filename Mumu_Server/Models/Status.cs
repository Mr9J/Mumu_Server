using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("Status")]
public partial class Status
{
    [Key]
    [Column("StatusID")]
    public int StatusId { get; set; }

    [StringLength(50)]
    public string StatusName { get; set; } = null!;

    [InverseProperty("Status")]
    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    [InverseProperty("Status")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("Status")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Status")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
