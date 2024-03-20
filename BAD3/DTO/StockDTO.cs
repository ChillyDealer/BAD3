using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
    public class StockDTO
    {
        [Required]
        public int StockId { get; set; }

        [Required]
        public int Quantity { get; set; }
    }
}