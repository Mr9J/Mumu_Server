using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Mumu_Server.Models;

public partial class MumuDbContext : DbContext
{
    public MumuDbContext()
    {
    }

    public MumuDbContext(DbContextOptions<MumuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<ActionDetail> ActionDetails { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AuthStatus> AuthStatuses { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartDetail> CartDetails { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<GroupDetail> GroupDetails { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<LikeDetail> LikeDetails { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberInterestProjectType> MemberInterestProjectTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<PaymentMethodId> PaymentMethodIds { get; set; }

    public virtual DbSet<PaymentStatusId> PaymentStatusIds { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectIdtype> ProjectIdtypes { get; set; }

    public virtual DbSet<ProjectType> ProjectTypes { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    public virtual DbSet<ShipmentStatus> ShipmentStatuses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:Mumu");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.Actions)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Actions_Projects");
        });

        modelBuilder.Entity<ActionDetail>(entity =>
        {
            entity.Property(e => e.ActionDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.Action).WithMany(p => p.ActionDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ActionDetails_Actions");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Admins)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admins_Members");
        });

        modelBuilder.Entity<AuthStatus>(entity =>
        {
            entity.Property(e => e.AuthStatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Carts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carts_Members");
        });

        modelBuilder.Entity<CartDetail>(entity =>
        {
            entity.HasOne(d => d.Cart).WithMany(p => p.CartDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetails_Carts");

            entity.HasOne(d => d.Status).WithMany(p => p.CartDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CartDetails_Status");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Members");

            entity.HasOne(d => d.Project).WithMany(p => p.Comments)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Projects");
        });

        modelBuilder.Entity<GroupDetail>(entity =>
        {
            entity.HasOne(d => d.AuthStatus).WithMany(p => p.GroupDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupDetails_AuthStatus");

            entity.HasOne(d => d.Group).WithMany(p => p.GroupDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupDetails_Groups");

            entity.HasOne(d => d.Member).WithMany(p => p.GroupDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GroupDetails_Members");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.Likes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Likes_Projects");
        });

        modelBuilder.Entity<LikeDetail>(entity =>
        {
            entity.HasOne(d => d.Like).WithMany(p => p.LikeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeDetails_Likes");

            entity.HasOne(d => d.Member).WithMany(p => p.LikeDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_LikeDetails_Members");
        });

        modelBuilder.Entity<MemberInterestProjectType>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.MemberInterestProjectTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberInterestProjectType_Members");

            entity.HasOne(d => d.ProjectType).WithMany(p => p.MemberInterestProjectTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberInterestProjectType_ProjectTypes");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Members");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_PaymentMethodID");

            entity.HasOne(d => d.PaymentStatus).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_PaymentStatusID");

            entity.HasOne(d => d.ShipmentStatus).WithMany(p => p.Orders)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_ShipmentStatus");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Orders");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Products");

            entity.HasOne(d => d.Project).WithMany(p => p.OrderDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Projects");
        });

        modelBuilder.Entity<PaymentMethodId>(entity =>
        {
            entity.Property(e => e.PaymentMethodId1).ValueGeneratedNever();
        });

        modelBuilder.Entity<PaymentStatusId>(entity =>
        {
            entity.Property(e => e.PaymentStatusId1).ValueGeneratedNever();
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.CurrentStock).IsFixedLength();

            entity.HasOne(d => d.Project).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Projects");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Status");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasOne(d => d.Group).WithMany(p => p.Projects).HasConstraintName("FK_Projects_Groups");

            entity.HasOne(d => d.Memeber).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Members");

            entity.HasOne(d => d.Status).WithMany(p => p.Projects)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Projects_Status");
        });

        modelBuilder.Entity<ProjectIdtype>(entity =>
        {
            entity.HasOne(d => d.Project).WithMany(p => p.ProjectIdtypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectIDType_Projects");

            entity.HasOne(d => d.ProjectType).WithMany(p => p.ProjectIdtypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectIDType_ProjectTypes");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasOne(d => d.Member).WithMany(p => p.Services).HasConstraintName("FK_Services_Members");

            entity.HasOne(d => d.Status).WithMany(p => p.Services).HasConstraintName("FK_Services_Status");
        });

        modelBuilder.Entity<ShipmentStatus>(entity =>
        {
            entity.Property(e => e.ShipmentStatusId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.Property(e => e.StatusId).ValueGeneratedNever();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
