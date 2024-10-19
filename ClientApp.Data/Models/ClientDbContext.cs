using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ClientApp.Data.Models;

public partial class ClientDbContext : DbContext
{
    public ClientDbContext()
    {
    }

    public ClientDbContext(DbContextOptions<ClientDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<TypeGender> TypeGenders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.ToTable("clients");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Client)
                .HasForeignKey<Client>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__clients__id__3E52440B");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__persons__3213E83F303D0B9F");

            entity.ToTable("persons");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("address");
            entity.Property(e => e.Document)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("document");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.TypeGenderId).HasColumnName("type_gender_id");
            entity.Property(e => e.Years).HasColumnName("years");

            entity.HasOne(d => d.TypeGender).WithMany(p => p.People)
                .HasForeignKey(d => d.TypeGenderId)
                .HasConstraintName("FK__persons__type_ge__3F466844");
        });

        modelBuilder.Entity<TypeGender>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__type_gen__3213E83FC72DE672");

            entity.ToTable("type_genders");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
