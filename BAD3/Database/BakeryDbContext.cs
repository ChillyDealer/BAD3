using Bad3.Model;
using Microsoft.EntityFrameworkCore;

namespace Bad3.Database;

public class BakeryDbContext : DbContext
{
	public BakeryDbContext(DbContextOptions<BakeryDbContext> options)
		: base(options)
	{
	}

	public DbSet<Customer> Customers => Set<Customer>();
	public DbSet<Order> Orders => Set<Order>();
	public DbSet<Goods> Goods => Set<Goods>();
	public DbSet<Batch> Batches => Set<Batch>();
	public DbSet<Ingredient> Ingredients => Set<Ingredient>();
	public DbSet<Stock> Stocks => Set<Stock>();
	public DbSet<Delivery> Deliveries => Set<Delivery>();
	public DbSet<Driver> Drivers => Set<Driver>();
	public DbSet<GoodsOrder> GoodsOrders => Set<GoodsOrder>();
	public DbSet<BatchGoods> BatchGoods => Set<BatchGoods>();
	public DbSet<IngredientBatch> IngredientBatches => Set<IngredientBatch>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		// Goods + Order
		modelBuilder.Entity<GoodsOrder>()
			.HasKey(go => new { go.GoodsId, go.OrderId });

		// Batch + Goods
		modelBuilder.Entity<BatchGoods>()
			.HasKey(bg => new { bg.BatchId, bg.GoodsId });

		// Ingredient + Batch
		modelBuilder.Entity<IngredientBatch>()
			.HasKey(ib => new { ib.IngredientId, ib.BatchId });
	}
}