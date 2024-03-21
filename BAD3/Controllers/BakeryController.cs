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
				Quantity = i.Stock.Quantity,
				Allergens = i.Allergens,
				
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
				Ingredients = new List<Ingredient>() // collection of ingredients
			};

			var ingredient = new Ingredient // assigning the new vars
			{
				Name = ingredientDto.Name,
				Stock = stock,
				Allergens = ingredientDto.Allergens
			};

			stock.Ingredients.Add(ingredient);

			_context.Stock.Add(stock); // add to database
			await _context.SaveChangesAsync();

			return Ok($"{ingredientDto.Name} added with quantity {ingredientDto.Quantity} and allergens {ingredientDto.Allergens}");
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
				ingredient.Allergens = ingredientDto.Allergens; // update allergens
				await _context.SaveChangesAsync(); // save
				return Ok($"{ingredient.Name} quantity updated to {ingredientDto.Quantity} and allergens to {ingredientDto.Allergens}");
			}

			return NotFound($"{ingredientDto.Name} not found");
		}

		// DELETE
		[HttpDelete("DeleteIngredient")]
		public async Task<IActionResult> DeleteIngredient([FromQuery] string name)
		{
			var ingredient = await _context.Ingredient
										  .Include(i => i.Stock)
										  .SingleOrDefaultAsync(i => i.Name == name);

			if (ingredient == null)
			{
				return NotFound($"{name} not found");
			}

			_context.Ingredient.Remove(ingredient);

			// see if others are linked to same stock
			bool hasOtherIngredients = await _context.Ingredient.AnyAsync(i => i.StockId == ingredient.StockId && i.IngredientId != ingredient.IngredientId);
			if (!hasOtherIngredients)
			{
				var stock = await _context.Stock.FindAsync(ingredient.StockId);
				if (stock != null)
				{
					// delete stock associated with ingredient
					_context.Stock.Remove(stock);
				}
			}

			await _context.SaveChangesAsync(); // commit delete

			return Ok($"{name} deleted");
		}
	}
}
