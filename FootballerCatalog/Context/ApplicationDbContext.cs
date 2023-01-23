using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {
    }

    public DbSet<Player> Players { get; set; }
}