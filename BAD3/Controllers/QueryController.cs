using Bad3.Database;
using Bad3.DTO;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Bad3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QueryController : ControllerBase
	{
		private readonly BakeryDbContext _context;

		public QueryController(BakeryDbContext context)
		{
			_context = context;
		}

		// Query 1: Get the current stock
		[HttpGet("GetCurrentStock")]
		public async Task<ActionResult<IEnumerable<StockDTO>>> GetCurrentStock()
		{
			var stocks = await _context.Stock
				.Select(s => new StockDTO
				{
					StockId = s.StockId,
					Quantity = s.Quantity
				}).ToListAsync();

			return Ok(stocks);
		}

		// Query 2: Get the address and date for an order
		[HttpGet("GetOrderDetails/{orderId}")]
		public async Task<ActionResult> GetOrderDetails(int orderId)
		{
			var orderDetails = await _context.Order
				.Where(o => o.OrderId == orderId)
				.Include(o => o.Delivery) // Include the delivery to access its address
				.Select(o => new { Address = o.Delivery.Address, o.OrderDate })
				.FirstOrDefaultAsync();

			if (orderDetails == null)
				return NotFound($"Details not found for order ID: {orderId}");

			return Ok(orderDetails);
		}

		// Query 3: Get the list of baked goods in an order
		[HttpGet("GetGoodsInOrder/{orderId from GoodsOrder}")]
		public async Task<ActionResult> GetGoodsInOrder(int orderId)
		{
			var goodsList = await _context.GoodsOrder
				.Where(go => go.OrderId == orderId)
				.Include(go => go.Goods)
				.Select(go => new { go.Goods.GoodName, go.Quantity })
				.ToListAsync();

			if (goodsList == null || !goodsList.Any())
				return NotFound($"No goods found for order ID: {orderId}");

			return Ok(goodsList);
		}

		// Query 4: Get the ingredients for a batch
		[HttpGet("GetIngredientsForBatch/{batchId}")]
		public async Task<ActionResult> GetIngredientsForBatch(int batchId)
		{
			var ingredients = await _context.IngredientBatch
				.Where(ib => ib.BatchId == batchId)
				.Include(ib => ib.Ingredients)
				.Select(ib => new { ib.Ingredients.Name, ib.Ingredients.Allergens, ib.Quantity })
				.ToListAsync();

			if (ingredients == null || !ingredients.Any())
				return NotFound($"No ingredients found for batch ID: {batchId}");

			return Ok(ingredients);
		}

		// Query 5: Get the track ids for order
		[HttpGet("GetTrackIdsForOrder/{orderId}")]
		public async Task<ActionResult> GetTrackIdsForOrder(int orderId)
		{
			var trackIds = await _context.Delivery
				.Where(d => d.OrderId == orderId)
				.Select(d => new {d.TrackId, d.Coordinates})
				.ToListAsync();

			if (trackIds == null || !trackIds.Any())
				return NotFound($"No track IDs found for order ID: {orderId}");

			return Ok(trackIds);
		}

		// Query 6: Get all quantities for goods
		[HttpGet("GetAllGoodsQuantities")]
		public async Task<ActionResult> GetAllGoodsQuantities()
		{
			var goodsQuantities = await _context.GoodsOrder
				.Include(go => go.Goods)
				.GroupBy(go => go.Goods.GoodName)
				.Select(g => new { GoodName = g.Key, TotalQuantity = g.Sum(go => go.Quantity) })
				.OrderBy(g => g.GoodName)
				.ToListAsync();

			return Ok(goodsQuantities);
		}

		// Query 7: Get the average delay 
		[HttpGet("GetAverageBatchDelay")]
		public async Task<ActionResult> GetAverageBatchDelay()
		{
			var averageDelay = await _context.Batch
				.AverageAsync(b => b.Delay);

			return Ok(new { AverageDelay = averageDelay });
		}
	}
}
