using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
	public class IngredientDTO
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
