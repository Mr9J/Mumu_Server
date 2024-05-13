using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Order
{
    [Key]
    [Column("OrderID")]
    public int OrderId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime OrderDate { get; set; }

    [Column("MemberID")]
    public int MemberId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ShipDate { get; set; }

    [Column("ShipmentStatusID")]
    public int ShipmentStatusId { get; set; }

    [Column("PaymentMethodID")]
    public int PaymentMethodId { get; set; }

    [Column("PaymentStatusID")]
    public int PaymentStatusId { get; set; }

    [ForeignKey("MemberId")]
    [InverseProperty("Orders")]
    public virtual Member Member { get; set; } = null!;

    [InverseProperty("Order")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("PaymentMethodId")]
    [InverseProperty("Orders")]
    public virtual PaymentMethodId PaymentMethod { get; set; } = null!;

    [ForeignKey("PaymentStatusId")]
    [InverseProperty("Orders")]
    public virtual PaymentStatusId PaymentStatus { get; set; } = null!;

    [ForeignKey("ShipmentStatusId")]
    [InverseProperty("Orders")]
    public virtual ShipmentStatus ShipmentStatus { get; set; } = null!;
}
