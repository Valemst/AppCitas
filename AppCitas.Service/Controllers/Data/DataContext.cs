using AppCitas.Service.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppCitas.Service.Controllers.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<AppUser> Users { get; set; }
}
