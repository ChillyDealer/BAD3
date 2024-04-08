using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad3.Model
{
	public class Batch
	{
		[Key]
		public int BatchId { get; set; }
		[Required]
		public string StartTime { get; set; }
		[Required]
		public string EndTime { get; set; }
		[Required]
		public int Delay { get; set; }
		public ICollection<IngredientBatch> IngredientBatch { get; set; }
	}
}
