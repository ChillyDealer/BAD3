using Bad3.Model;
using Microsoft.EntityFrameworkCore;

namespace Bad3.Database;

public class BakeryDbContext : DbContext
{
    public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
        : base(options)
    {
    }

    public DbSet<Order> Orders => Set<Order>();
}