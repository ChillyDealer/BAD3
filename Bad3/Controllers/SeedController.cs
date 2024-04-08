using System.Collections.ObjectModel;
using Bad3.Database;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bad3.Controllers;

[Route("[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
	private BakeryDbContext _context;

	public SeedController(BakeryDbContext context)
	{
		_context = context;
	}

	[HttpPost]
	public async Task<IActionResult> SeedDb()
	{
		var order = new Order
		{
			OrderDate = "08:04:2024 14:26"
		};

		var customer = new Customer
		{
			CustomerName = "Hej",
			Orders = new Collection<Order>()
			{
				order
			}
		};
		if (!_context.Customer.Any(c => c.CustomerID == customer.CustomerID))
		{
			await _context.Customer.AddAsync(customer);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("Customer already exists.");
		}

		var good = new Goods
		{
			GoodName = "Strawberry Cake",
			Validity = "16:08:2024",
			Quantity = 40
		};
		if (!_context.Goods.Any(e => e.GoodsId == good.GoodsId))
		{
			await _context.Goods.AddAsync(good);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("Good already exists.");
		}

		var delivery = new Delivery
		{
			TrackId = "1",
			Address = "Gammelvej 4",
			Coordinates = "18.299803, -64.825582",
			Date = "08:04:2024 15:30",
			Order = order // order use address
		};

		var driver = new Driver
		{
			Name = "Bossman",
			Deliveries = new List<Delivery>
			{
				delivery
			}
		};
		if (!_context.Driver.Any(e => e.DriverId == driver.DriverId))
		{
			await _context.Driver.AddAsync(driver);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("Driver already exists.");
		}

		var batch = new Batch
		{
			StartTime = "08:04:2024 14:10",
			EndTime = "03:05:2024 11:20",
			Delay = 6

		};
		if (!_context.Batch.Any(e => e.BatchId == batch.BatchId))
		{
			await _context.Batch.AddAsync(batch);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("batch already exists.");
		}

		var ingredient = new Ingredient
		{
			Name = "Sugar",
			Allergens = "Nuts",
			StockId = 1
		};

		var stock = new Stock
		{
			Name = "Bakery stock",
			Ingredients = new List<Ingredient>
			{
				ingredient
			}
		};
		if (!_context.Stock.Any(e => e.StockId == stock.StockId))
		{
			await _context.Stock.AddAsync(stock);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("stock already exists.");
		}

		#region Relations

		var goodsOrder = new GoodsOrder
		{
			GoodsId = good.GoodsId,
			OrderId = order.OrderId,
			Quantity = 20
		};

		if (!_context.GoodsOrder.Any(e => e.GoodsId == goodsOrder.GoodsId && e.OrderId == goodsOrder.OrderId))
		{
			await _context.GoodsOrder.AddAsync(goodsOrder);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("GoodsOrder already exists.");
		}

		var ingredientBatch = new IngredientBatch
		{
			BatchId = 1,
			IngredientId = ingredient.IngredientId,
			Quantity = 80
		};

		if (!_context.IngredientBatch.Any(e => e.BatchId == ingredientBatch.BatchId) &&
			!_context.IngredientBatch.Any(e => e.IngredientId == ingredientBatch.IngredientId))
		{
			await _context.IngredientBatch.AddAsync(ingredientBatch);
			await _context.SaveChangesAsync();
		}
		else
		{
			Console.WriteLine("ignreindbatb already exists.");
		}

		#endregion

		return CreatedAtAction(nameof(SeedDb), new { id = customer.CustomerID }, customer);
	}
}