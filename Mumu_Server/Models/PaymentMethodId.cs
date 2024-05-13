using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

[Table("PaymentMethodID")]
public partial class PaymentMethodId
{
    [Key]
    [Column("PaymentMethodID")]
    public int PaymentMethodId1 { get; set; }

    [StringLength(50)]
    public string? PaymentName { get; set; }

    [InverseProperty("PaymentMethod")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
