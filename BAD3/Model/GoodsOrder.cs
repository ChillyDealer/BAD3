using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3.Model
{
	public class GoodsOrder
	{
		[ForeignKey("Order")]
		public int OrderId { get; set; }
		public Order Order { get; set; }

		[ForeignKey("Goods")]
		public int GoodsId { get; set; }
		public Goods Goods { get; set; }

		[Required]
		public int Quantity { get; set; }
	}
}
