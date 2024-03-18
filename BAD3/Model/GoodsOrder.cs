using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3.Model
{
	public class GoodsOrder
	{
		[Key, ForeignKey("Goods")]
		public int GoodsId { get; set; }
		public virtual Goods Goods { get; set; }

		[Key, ForeignKey("Order")]
		public int OrderId { get; set; }
		public virtual Order Order { get; set; }
	}
}
