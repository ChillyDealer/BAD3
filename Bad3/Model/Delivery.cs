using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	internal class Delivery
	{
		[Key]
		public int DeliveryId { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string TrackID { get; set; }
		[Required]
		public DateTime Date { get; set; }
		[ForeignKey("Driver")]
		public int DriverId { get; set; }
		public virtual Driver Driver { get; set; }
	}
}
