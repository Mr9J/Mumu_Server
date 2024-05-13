using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class CartDetail
{
    [Key]
    [Column("CartDetailID")]
    public int CartDetailId { get; set; }

    [Column("CartID")]
    public int CartId { get; set; }

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [Column("ProductID")]
    public int ProductId { get; set; }

    public int Count { get; set; }

    [Column(TypeName = "money")]
    public decimal Price { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    [ForeignKey("CartId")]
    [InverseProperty("CartDetails")]
    public virtual Cart Cart { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("CartDetails")]
    public virtual Status Status { get; set; } = null!;
}
