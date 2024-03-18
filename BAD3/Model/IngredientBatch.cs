using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3.Model
{
	public class IngredientBatch
	{
		[Key, ForeignKey("Ingredient")]
		public int IngredientId { get; set; }
		public virtual Ingredient Ingredient { get; set; }

		[Key, ForeignKey("Batch")]
		public int BatchId { get; set; }
		public virtual Batch Batch { get; set; }
	}
}
