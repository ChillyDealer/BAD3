using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
	public class OrderDTO
	{
		[Required]
		public int OrderId { get; set; }

		[Required]
		public string OrderDate { get; set; }
	}
}