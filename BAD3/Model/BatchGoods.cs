using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bad3.Model
{
	public class BatchGoods
	{
		[Key, ForeignKey("Batch")]
		public int BatchId { get; set; }
		public virtual Batch Batch { get; set; }
		[Key, ForeignKey("Goods")]
		public int GoodsId { get; set; }
		public virtual Goods Goods { get; set; }
	}
}
