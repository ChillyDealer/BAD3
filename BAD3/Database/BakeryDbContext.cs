using Bad3.Model;
using Microsoft.EntityFrameworkCore;

namespace Bad3.Database;

public class BakeryDbContext : DbContext
{
	public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
		: base(options)
	{
	}

	public DbSet<Customer> Customer => Set<Customer>();
	public DbSet<Order> Order => Set<Order>();
	public DbSet<Goods> Goods => Set<Goods>();
	public DbSet<Batch> Batch => Set<Batch>();
	public DbSet<Ingredient> Ingredient => Set<Ingredient>();
	public DbSet<Stock> Stock => Set<Stock>();
	public DbSet<Delivery> Delivery => Set<Delivery>();
	public DbSet<Driver> Driver => Set<Driver>();
	public DbSet<GoodsOrder> GoodsOrder => Set<GoodsOrder>();
	public DbSet<IngredientBatch> IngredientBatch => Set<IngredientBatch>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Goods + Order
		modelBuilder.Entity<GoodsOrder>()
			.HasKey(go => new { go.GoodsId, go.OrderId });

		// Ingredient + Batch
		modelBuilder.Entity<IngredientBatch>()
			.HasKey(ib => new { ib.IngredientId, ib.BatchId });
	}
}