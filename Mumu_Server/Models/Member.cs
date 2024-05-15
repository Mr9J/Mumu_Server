using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class Member
{
    [Key]
    [Column("MemberID")]
    public int MemberId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Unicode(false)]
    public string Password { get; set; } = null!;

    [StringLength(50)]
    public string Nickname { get; set; } = null!;

    [StringLength(100)]
    public string? Thumbnail { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(100)]
    public string? Address { get; set; }

    public string? MemberIntroduction { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? RegistrationTime { get; set; }

    [InverseProperty("Member")]
    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();

    [InverseProperty("Member")]
    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    [InverseProperty("Member")]
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    [InverseProperty("Member")]
    public virtual ICollection<GroupDetail> GroupDetails { get; set; } = new List<GroupDetail>();

    [InverseProperty("Member")]
    public virtual ICollection<LikeDetail> LikeDetails { get; set; } = new List<LikeDetail>();

    [InverseProperty("Member")]
    public virtual ICollection<MemberInterestProjectType> MemberInterestProjectTypes { get; set; } = new List<MemberInterestProjectType>();

    [InverseProperty("Member")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    [InverseProperty("Memeber")]
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    [InverseProperty("Member")]
    public virtual ICollection<Service> Services { get; set; } = new List<Service>();
}
