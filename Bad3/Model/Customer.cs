using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Customer
	{
		[Key]
		public int CustomerID { get; set; }

		[Required]
		public string CustomerName { get; set; } = string.Empty;
		public virtual ICollection<Order> Orders { get; set; }
	}
}
