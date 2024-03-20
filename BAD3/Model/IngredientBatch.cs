using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3.Model
{
	public class IngredientBatch
	{
		[ForeignKey("Batch")]
		public int BatchId { get; set; }
		public Batch Batch { get; set; }

		[ForeignKey("Ingredients")]
		public int IngredientId { get; set; }
		public Ingredient Ingredients { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
