using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AccountApp.Data.Models;

public partial class AccountDbContext : DbContext
{
    public AccountDbContext()
    {
    }

    public AccountDbContext(DbContextOptions<AccountDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<Movement> Movements { get; set; }

    public virtual DbSet<TypeAccount> TypeAccounts { get; set; }

    public virtual DbSet<TypeMovement> TypeMovements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=DatabaseConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__accounts__3213E83FB8100209");

            entity.ToTable("accounts");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("account_number");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.OpeningBalance)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("opening_balance");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TypeAccountId).HasColumnName("type_account_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__client__48CFD27E");

            entity.HasOne(d => d.TypeAccount).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.TypeAccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__accounts__type_a__4316F928");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__clients__3213E83F705FF34B");

            entity.ToTable("clients");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("full_name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Movement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__movement__3213E83F4715335B");

            entity.ToTable("movements");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("id");
            entity.Property(e => e.AccountId).HasColumnName("account_id");
            entity.Property(e => e.Balance)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("balance");
            entity.Property(e => e.Date)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.TypeMovementId).HasColumnName("type_movement_id");
            entity.Property(e => e.Value)
                .HasColumnType("decimal(19, 4)")
                .HasColumnName("value");

            entity.HasOne(d => d.Account).WithMany(p => p.Movements)
                .HasForeignKey(d => d.AccountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movements__accou__440B1D61");

            entity.HasOne(d => d.TypeMovement).WithMany(p => p.Movements)
                .HasForeignKey(d => d.TypeMovementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__movements__type___44FF419A");
        });

        modelBuilder.Entity<TypeAccount>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__type_acc__3213E83F2CA31C63");

            entity.ToTable("type_accounts");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("description");
        });

        modelBuilder.Entity<TypeMovement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__type_mov__3213E83F9C4DAB31");

            entity.ToTable("type_movements");

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
