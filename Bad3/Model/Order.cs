using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }
		[Required]
		public DateTime OrderDate { get; set; }
		[ForeignKey("Customer")]
		public int CustomerId { get; set; }
		public Customer Customer { get; set; }
		public ICollection<GoodsOrder> GoodsOrders { get; set; }
	}
}
