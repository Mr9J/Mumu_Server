using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Service
{
    [Key]
    [Column("ServiceID")]
    public int ServiceId { get; set; }

    [Column("MemberID")]
    public int? MemberId { get; set; }

    public string? ServiceMsg { get; set; }

    [Column("StatusID")]
    public int? StatusId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Date { get; set; }

    [ForeignKey("MemberId")]
    [InverseProperty("Services")]
    public virtual Member? Member { get; set; }

    [ForeignKey("StatusId")]
    [InverseProperty("Services")]
    public virtual Status? Status { get; set; }
}
