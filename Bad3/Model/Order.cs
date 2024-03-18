using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	internal class Order
	{
		[Key]
		public int OrderId { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public virtual Customer Customer { get; set; }
		public virtual ICollection<GoodsOrder> GoodsOrders { get; set; }
	}
}
