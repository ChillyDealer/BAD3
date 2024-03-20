using Bad3.Database;
using Bad3.DTO;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad3.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class StockController : ControllerBase
	{
		private readonly BakeryDbContext _context;

		public StockController(BakeryDbContext context)
		{
			_context = context;
		}

		// GET: ALL
		[HttpGet("GetAllIngredients")]
		public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetAllIngredients()
		{
			var ingredients = await _context.Ingredient.Include(i => i.Stock).Select(i => new IngredientDTO
			{
				Name = i.Name,
				Quantity = i.Stock.Quantity
			}).ToListAsync();

			return Ok(ingredients);
		}

		// POST
		[HttpPost("AddIngredient")]
		public async Task<ActionResult> AddIngredient([FromBody] IngredientDTO ingredientDto)
		{
			if (ingredientDto.Quantity < 0)
				return BadRequest("negativ number");

			var stock = new Stock
			{
				Name = ingredientDto.Name,
				Quantity = ingredientDto.Quantity,
				Ingredients = new List<Ingredient>() // collection of intredients
			};

			var ingredient = new Ingredient // assigning the new vars
			{
				Name = ingredientDto.Name,
				Stock = stock
			};

			stock.Ingredients.Add(ingredient);

			_context.Stock.Add(stock); // add to database
			await _context.SaveChangesAsync();

			return Ok($"{ingredientDto.Name} added with quantity {ingredientDto.Quantity}");
		}

		// PUT
		[HttpPut("UpdateIngredient")]
		public async Task<IActionResult> UpdateIngredient([FromBody] IngredientDTO ingredientDto)
		{
			if (ingredientDto.Quantity < 0)
				return BadRequest("negativ number");

			var stock = await _context.Stock
				.Include(s => s.Ingredients)
				.FirstOrDefaultAsync(s => s.Ingredients.Any(i => i.Name == ingredientDto.Name));

			if (stock == null)
				return NotFound($"{ingredientDto.Name} not found");

			var ingredient = stock.Ingredients.FirstOrDefault(i => i.Name == ingredientDto.Name);
			if (ingredient != null)
			{
				stock.Quantity = ingredientDto.Quantity; // update quant
				await _context.SaveChangesAsync(); // save
				return Ok($"{ingredient.Name} quantity updated to {ingredientDto.Quantity}");
			}

			return NotFound($"{ingredientDto.Name} not found");
		}

		// DELETE
		[HttpDelete("DeleteIngredient")]
		public async Task<IActionResult> DeleteIngredient([FromQuery] string name)
		{
			var stock = await _context.Stock
				.Include(s => s.Ingredients)
				.FirstOrDefaultAsync(s => s.Ingredients.Any(i => i.Name == name)); // find the item

			if (stock == null)
				return NotFound($"{name} not found");

			var ingredient = stock.Ingredients.FirstOrDefault(i => i.Name == name);
			if (ingredient != null)
			{
				_context.Ingredient.Remove(ingredient);
				await _context.SaveChangesAsync(); // commit delete
				return Ok($"{name} deleted");
			}

			return NotFound($"{name} not found");
		}
	}
}
