using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
	public class BatchDTO
	{
		[Required]
		public int BatchId { get; set; }

		[Required]
		public string StartTime { get; set; }

		[Required]
		public string EndTime { get; set; }

		public string Delay { get; set; }
	}
}