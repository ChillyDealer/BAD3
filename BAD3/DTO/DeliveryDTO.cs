using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
	public class DeliveryDTO
	{
		[Required]
		public int TrackId { get; set; }

		[Required]
		public string Address { get; set; }

		[Required]
		public string Coordinates { get; set; }

		[Required]
		public string Date { get; set; }
	}
}