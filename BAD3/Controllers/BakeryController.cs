using Bad3.Database;
using Bad3.DTO;
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

	// GET: bakery/ingredient?name=flour
	[HttpGet("ingredient")]
	public async Task<ActionResult<Ingredient>> GetIngredient(string name)
	{
		var ingredient = await _context.Ingredient
			.Include(i => i.Stock)
			.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());

		if (ingredient == null)
		{
			return NotFound();
		}

		return ingredient;
	}

	// POST: bakery/ingredient
	[HttpPost("ingredient")]
	public async Task<ActionResult<Ingredient>> PostIngredient([FromBody] IngredientDTO ingredientDTO)
	{
		if (ingredientDTO == null || string.IsNullOrEmpty(ingredientDTO.Name))
		{
			return BadRequest("Invalid ingredient data.");
		}

		if (_context.Stock.Any(s => s.Name.ToLower() == ingredientDTO.Name.ToLower()))
		{
			return BadRequest("Ingredient already exists.");
		}

		var stock = new Stock
		{
			Name = ingredientDTO.Name,
			Quantity = ingredientDTO.Quantity
		};

		var ingredient = new Ingredient
		{
			Name = ingredientDTO.Name,
			Stock = stock
		};

		_context.Ingredient.Add(ingredient);
		await _context.SaveChangesAsync();

		return CreatedAtAction(nameof(GetIngredient), new { name = ingredient.Name }, ingredient);
	}


	// PUT: bakery/ingredient?name=flour
	[HttpPut("ingredient")]
	public async Task<IActionResult> PutIngredient(string name, int quantity)
	{
		var ingredient = await _context.Ingredient
			.Include(i => i.Stock)
			.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());

		if (ingredient == null)
		{
			return NotFound();
		}

		ingredient.Stock.Quantity = quantity;
		_context.Entry(ingredient).State = EntityState.Modified;

		try
		{
			await _context.SaveChangesAsync();
		}
		catch (DbUpdateConcurrencyException)
		{
			if (!IngredientExists(ingredient.IngredientId))
			{
				return NotFound();
			}
			else
			{
				throw;
			}
		}

		return NoContent();
	}

	// DELETE: bakery/ingredient?name=flour
	[HttpDelete("ingredient")]
	public async Task<IActionResult> DeleteIngredient(string name)
	{
		var ingredient = await _context.Ingredient
			.FirstOrDefaultAsync(i => i.Name.ToLower() == name.ToLower());

		if (ingredient == null)
		{
			return NotFound();
		}

		_context.Ingredient.Remove(ingredient);
		await _context.SaveChangesAsync();

		return NoContent();
	}

	private bool IngredientExists(int id)
	{
		return _context.Ingredient.Any(e => e.IngredientId == id);
	}
}
