using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EntityModel.SqlServer;

public partial class RanRanJiaBlogContext : DbContext
{
    public RanRanJiaBlogContext()
    {
    }

    public RanRanJiaBlogContext(DbContextOptions<RanRanJiaBlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminInfo> AdminInfos { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=JIAJIA\\JIAJIA;Initial Catalog=RanRanJiaBlog;User ID=sa;Password=123456;Trusted_Connection=False;MultipleActiveResultSets=true;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Account)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EMail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("E_Mail");
            entity.Property(e => e.ExpirationTime).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AdminInfo>(entity =>
        {
            entity.ToTable("AdminInfo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.NickName).HasMaxLength(50);

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminInfos)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_Admin_AdminInfo");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Category).HasMaxLength(500);
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreateBy).HasMaxLength(500);
            entity.Property(e => e.CreateTime).HasColumnType("datetime");
            entity.Property(e => e.Img)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Tag).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(500);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("Log");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Ip)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("IP");
            entity.Property(e => e.Time).HasColumnType("datetime");
            entity.Property(e => e.What).HasMaxLength(50);
            entity.Property(e => e.Who).HasMaxLength(50);
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.TagName).HasMaxLength(50);
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.ExpirationTime).HasColumnType("datetime");
            entity.Property(e => e.Token1)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Token");

            entity.HasOne(d => d.Admin).WithMany(p => p.Tokens)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_Admin_Token");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
