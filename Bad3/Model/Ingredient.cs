using Bad3.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3;
public class Ingredient
{
	[Key]
	public int IngredientId { get; set; }
	[Required]
	public string Name { get; set; }
	[Required]
	public string Allergens { get; set; }

	[ForeignKey("Stock")]
	public int StockId { get; set; }
	public virtual Stock Stock { get; set; }
	public ICollection<IngredientBatch> IngredientBatch { get; set; }
}
