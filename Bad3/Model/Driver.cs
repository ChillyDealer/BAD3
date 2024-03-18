using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Driver
	{
		[Key]
		public int DriverId { get; set; }
		[Required]
		public string Name { get; set; }
		public virtual ICollection<Delivery> Deliveries { get; set; }
	}
}
