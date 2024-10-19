using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CuentaAPI.Models;

public partial class CuentaDbContext : DbContext
{
    public CuentaDbContext()
    {
    }

    public CuentaDbContext(DbContextOptions<CuentaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<Movimiento> Movimientos { get; set; }

    public virtual DbSet<TiposCuenta> TiposCuentas { get; set; }

    public virtual DbSet<TiposMovimiento> TiposMovimientos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__cuentas__3213E83F1C3B0E34");

            entity.ToTable("cuentas");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.ClienteId).HasColumnName("cliente_id");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NumeroCuenta)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("numero_cuenta");
            entity.Property(e => e.SaldoInicial)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("saldo_inicial");
            entity.Property(e => e.TipoCuentaId).HasColumnName("tipo_cuenta_id");

            entity.HasOne(d => d.TipoCuenta).WithMany(p => p.Cuenta)
                .HasForeignKey(d => d.TipoCuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__cuentas__tipo_cu__4316F928");
        });

        modelBuilder.Entity<Movimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movimien__3213E83F5864FD62");

            entity.ToTable("movimientos");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.CuentaId).HasColumnName("cuenta_id");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Saldo)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("saldo");
            entity.Property(e => e.TipoMovimientoId).HasColumnName("tipo_movimiento_id");
            entity.Property(e => e.Valor)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("valor");

            entity.HasOne(d => d.Cuenta).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.CuentaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movimient__cuent__440B1D61");

            entity.HasOne(d => d.TipoMovimiento).WithMany(p => p.Movimientos)
                .HasForeignKey(d => d.TipoMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movimient__tipo___44FF419A");
        });

        modelBuilder.Entity<TiposCuenta>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipos_cu__3213E83F6A3F7513");

            entity.ToTable("tipos_cuentas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<TiposMovimiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tipos_mo__3213E83F172B7615");

            entity.ToTable("tipos_movimientos");

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
