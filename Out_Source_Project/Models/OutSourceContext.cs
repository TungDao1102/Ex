using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Out_Source_Project.Models;

public partial class OutSourceContext : DbContext
{
    public OutSourceContext()
    {
    }

    public OutSourceContext(DbContextOptions<OutSourceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MR_TUNG_PC\\TUNGDAO;Initial Catalog=OutSource;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK_Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(50);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Salt)
                .HasMaxLength(10)
                .IsFixedLength();

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Accounts_Roles");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK_Categoriess");

            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.Alias).HasMaxLength(255);
            entity.Property(e => e.CatName).HasMaxLength(255);
            entity.Property(e => e.Cover).HasMaxLength(255);
            entity.Property(e => e.Icon).HasMaxLength(255);
            entity.Property(e => e.MetaDesc).HasMaxLength(255);
            entity.Property(e => e.MetaKey).HasMaxLength(255);
            entity.Property(e => e.Thumb).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MsgId);

            entity.ToTable("Message");

            entity.Property(e => e.MsgId).HasColumnName("MsgID");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Message1).HasColumnName("Message");
            entity.Property(e => e.Number).HasMaxLength(50);
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("PK_Post");

            entity.Property(e => e.PostId).HasColumnName("PostID");
            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Alias).HasMaxLength(255);
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsHot).HasColumnName("isHot");
            entity.Property(e => e.IsNewfeed).HasColumnName("isNewfeed");
            entity.Property(e => e.MetaDesc).HasMaxLength(255);
            entity.Property(e => e.MetaKey).HasMaxLength(255);
            entity.Property(e => e.MetaTitle).HasMaxLength(255);
            entity.Property(e => e.Scontents).HasColumnName("SContents");
            entity.Property(e => e.Thumb).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Account).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK_Posts_Accounts");

            entity.HasOne(d => d.Cat).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CatId)
                .HasConstraintName("FK_Posts_Categories");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK_Roless");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.RoleDescription).HasMaxLength(50);
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
