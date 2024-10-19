using Microsoft.EntityFrameworkCore;

namespace ClientApp.Data.Models;

public partial class ClientDbContext : DbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
        .ToTable("Clients")
        .HasBaseType<Person>();
    }
}
