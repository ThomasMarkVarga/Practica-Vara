using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DecontDbContext.Models;

public partial class DecontDbContextCs : DbContext
{
    public DecontDbContextCs()
    {
    }

    public DecontDbContextCs(DbContextOptions<DecontDbContextCs> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }

    public virtual DbSet<RandDocument> RandDocuments { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TipCheltuiala> TipCheltuialas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQlExpress;Database=DecontDB;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC27E326809E");

            entity.ToTable("Document");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Explicatie).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Status).WithMany(p => p.Documents)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_status");
        });

        modelBuilder.Entity<RandDocument>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RandDocu__3214EC270FBE8893");

            entity.ToTable("RandDocument");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CheltuialaId).HasColumnName("CheltuialaID");
            entity.Property(e => e.DocumentId).HasColumnName("DocumentID");
            entity.Property(e => e.Explicatie).HasColumnType("text");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Valoare).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.Cheltuiala).WithMany(p => p.RandDocuments)
                .HasForeignKey(d => d.CheltuialaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_cheltuiala");

            entity.HasOne(d => d.Document).WithMany(p => p.RandDocuments)
                .HasForeignKey(d => d.DocumentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_document");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Status__3214EC275D46B502");

            entity.ToTable("Status");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Status1)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Status");
        });

        modelBuilder.Entity<TipCheltuiala>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TipChelt__3214EC278EBB9934");

            entity.ToTable("TipCheltuiala");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Denumire)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.ValoareImplicita).HasColumnType("decimal(10, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
