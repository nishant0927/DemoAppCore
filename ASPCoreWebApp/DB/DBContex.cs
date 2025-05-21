using System;
using System.Collections.Generic;
using ASPCoreWebApp.DB.Table;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebApp.DB;

public partial class DBContex : DbContext
{
    public DBContex()
    {
    }

    public DBContex(DbContextOptions<DBContex> options)
        : base(options)
    {
    }

    public virtual DbSet<TableEmployee> TableEmployees { get; set; }

    public virtual DbSet<TblDepartment> TblDepartments { get; set; }

    public virtual DbSet<TblDesignationMaster> TblDesignationMasters { get; set; }

    public virtual DbSet<TblFile> TblFiles { get; set; }

    public virtual DbSet<TblItemMaster> TblItemMasters { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-T5RNCLV\\SA;Database=DemoDB;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TableEmployee>(entity =>
        {
            entity.HasKey(e => e.EmpGuid).HasName("PK_TableEmployee_1");

            entity.ToTable("TableEmployee");

            entity.Property(e => e.EmpGuid)
                .ValueGeneratedNever()
                .HasColumnName("EmpGUId");
            entity.Property(e => e.EmpName).HasMaxLength(50);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.EmpDepartmentNavigation).WithMany(p => p.TableEmployees)
                .HasForeignKey(d => d.EmpDepartment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableEmployee_TblDepartment");

            entity.HasOne(d => d.EmpDesignationNavigation).WithMany(p => p.TableEmployees)
                .HasForeignKey(d => d.EmpDesignation)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TableEmployee_TblDesignationMaster");
        });

        modelBuilder.Entity<TblDepartment>(entity =>
        {
            entity.HasKey(e => e.DepartmentId);

            entity.ToTable("TblDepartment");

            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblDesignationMaster>(entity =>
        {
            entity.HasKey(e => e.DesignationId);

            entity.ToTable("TblDesignationMaster");

            entity.Property(e => e.DesignationName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblFile>(entity =>
        {
            entity.ToTable("TblFile");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EmpGuid).HasColumnName("EmpGUId");
            entity.Property(e => e.FileGuid).HasColumnName("FileGUId");
            entity.Property(e => e.FileName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FilePath)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.Emp).WithMany(p => p.TblFiles)
                .HasForeignKey(d => d.EmpGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_TblFile_TblFile");
        });

        modelBuilder.Entity<TblItemMaster>(entity =>
        {
            entity.ToTable("Tbl_ItemMaster");

            entity.Property(e => e.ItemCode).HasMaxLength(50);
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ItemName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ItemUmo)
                .HasMaxLength(20)
                .HasColumnName("ItemUMO");
            entity.Property(e => e.ItemUnitPrice).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
