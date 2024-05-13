using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Product
{
    [Key]
    [Column("ProductID")]
    public int ProductId { get; set; }

    [StringLength(100)]
    public string ProductName { get; set; } = null!;

    [Column("ProjectID")]
    public int ProjectId { get; set; }

    [Column(TypeName = "money")]
    public decimal OnSalePrice { get; set; }

    [Column(TypeName = "money")]
    public decimal ProductPrice { get; set; }

    public string ProductDescription { get; set; } = null!;

    public int InitialStock { get; set; }

    [StringLength(10)]
    public string CurrentStock { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    [StringLength(100)]
    public string? Thumbnail { get; set; }

    [Column("StatusID")]
    public int StatusId { get; set; }

    public int? OrderBy { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    [ForeignKey("ProjectId")]
    [InverseProperty("Products")]
    public virtual Project Project { get; set; } = null!;

    [ForeignKey("StatusId")]
    [InverseProperty("Products")]
    public virtual Status Status { get; set; } = null!;
}
