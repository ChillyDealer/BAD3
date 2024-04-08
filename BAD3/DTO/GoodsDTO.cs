using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
	public class GoodsDTO
	{
		[Required]
		public string GoodName { get; set; }

		[Required]
		public int Quantity { get; set; }

		public string Validity { get; set; }
	}
}