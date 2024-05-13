using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("ShipmentStatus")]
public partial class ShipmentStatus
{
    [Key]
    [Column("ShipmentStatusID")]
    public int ShipmentStatusId { get; set; }

    [StringLength(50)]
    public string ShipmentStatusName { get; set; } = null!;

    [InverseProperty("ShipmentStatus")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
