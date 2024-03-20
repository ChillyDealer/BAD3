using Bad3.Database;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Bad3.Controllers;

[Route("[controller]")]
[ApiController]
public class BakeryController : ControllerBase
{
	private readonly BakeryDbContext _context;

	public BakeryController(BakeryDbContext context)
	{
		_context = context;
	}

	// Add a new ingredient with stock quantity
	[HttpPost]
	public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredient, int stockQuantity)
	{
		var existingStock = await _context.Stocks.FirstOrDefaultAsync(s => s.Name == ingredient.Name);
		if (existingStock == null)
		{
			var newStock = new Stock { Name = ingredient.Name, Quantity = stockQuantity };
			_context.Stocks.Add(newStock);
			ingredient.Stock = newStock;
		}
		else
		{
			existingStock.Quantity += stockQuantity;
			ingredient.Stock = existingStock;
		}

		_context.Ingredients.Add(ingredient);
		await _context.SaveChangesAsync();
		return CreatedAtAction(nameof(GetIngredientByName), new { name = ingredient.Name }, ingredient);
	}

	// Get an ingredient by Name
	[HttpGet("{name}")]
	public async Task<ActionResult<Ingredient>> GetIngredientByName(string name)
	{
		var ingredient = await _context.Ingredients
									  .Include(i => i.Stock)
									  .Where(i => i.Name == name)
									  .FirstOrDefaultAsync();
		if (ingredient == null)
		{
			return NotFound();
		}
		return ingredient;
	}

	// Update an ingredient's stock by Name
	[HttpPut("{name}")]
	public async Task<IActionResult> UpdateIngredientStock(string name, int newStockQuantity)
	{
		var ingredient = await _context.Ingredients
									  .Include(i => i.Stock)
									  .Where(i => i.Name == name)
									  .FirstOrDefaultAsync();
		if (ingredient == null || ingredient.Stock == null)
		{
			return NotFound();
		}

		ingredient.Stock.Quantity = newStockQuantity;
		_context.Entry(ingredient.Stock).State = EntityState.Modified;
		await _context.SaveChangesAsync();

		return NoContent();
	}

	// Delete an ingredient by Name
	[HttpDelete("{name}")]
	public async Task<IActionResult> DeleteIngredientByName(string name)
	{
		var ingredient = await _context.Ingredients
									  .Where(i => i.Name == name)
									  .FirstOrDefaultAsync();
		if (ingredient == null)
		{
			return NotFound();
		}

		_context.Ingredients.Remove(ingredient);
		await _context.SaveChangesAsync();

		return NoContent();
	}
}
