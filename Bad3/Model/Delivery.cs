using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Delivery
	{
		[Key]
		public int DeliveryId { get; set; }

		[Required]
		public string TrackId { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string Coordinates { get; set; }

		[Required]
		public string Date { get; set; }

		[ForeignKey("Driver")]
		public int DriverId { get; set; }

		[ForeignKey("Order")]
		public int OrderId { get; set; }

		// Address for order
		public virtual Order Order { get; set; }
	}
}
