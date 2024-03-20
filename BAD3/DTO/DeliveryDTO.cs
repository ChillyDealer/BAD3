using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
    public class DeliveryDTO
    {
        [Required]
        public int TrackId { get; set; }

        public string Address { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}