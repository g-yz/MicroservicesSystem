using ClienteApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ClienteAPI.Models;

public partial class ClienteDbContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>()
        .ToTable("Clientes")
        .HasBaseType<Persona>();
    }
}
