using System;
using System.Collections.Generic;
using ClienteApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Models;

public partial class ClienteDbContext : DbContext
{
    public ClienteDbContext()
    {
    }

    public ClienteDbContext(DbContextOptions<ClienteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Persona> Personas { get; set; }

    public virtual DbSet<TiposGenero> TiposGeneros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("clientes");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("password");

            entity.HasOne(d => d.Persona).WithOne(p => p.Cliente)
                .HasForeignKey<Cliente>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__clientes__id__3E52440B");
        });

        modelBuilder.Entity<Persona>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__personas__3213E83FE75B4738");

            entity.ToTable("personas");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Identificacion)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("identificacion");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoGeneroId).HasColumnName("tipo_genero_id");

            entity.HasOne(d => d.TipoGenero).WithMany(p => p.Personas)
                .HasForeignKey(d => d.TipoGeneroId)
                .HasConstraintName("FK__personas__tipo_g__3F466844");
        });

        modelBuilder.Entity<TiposGenero>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipos_ge__3213E83F6E7B6E13");

            entity.ToTable("tipos_generos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
