using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Stock
	{
		[Key]
		public int StockId { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Quantity { get; set; }
		public virtual ICollection<Ingredient> Ingredients { get; set; }

	}
}
