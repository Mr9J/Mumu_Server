using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("PaymentStatusID")]
public partial class PaymentStatusId
{
    [Key]
    [Column("PaymentStatusID")]
    public int PaymentStatusId1 { get; set; }

    [StringLength(50)]
    public string? PaymentStatusName { get; set; }

    [InverseProperty("PaymentStatus")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
