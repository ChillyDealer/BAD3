using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{

	public class Goods
	{
		[Key]
		public int GoodsId { get; set; }
		[Required]
		public string GoodName { get; set; }
		[Required]
		public string Validity { get; set; }
		[Required]
		public int Quantity { get; set; }
		public virtual ICollection<GoodsOrder> GoodsOrders { get; set; }
	}
}
