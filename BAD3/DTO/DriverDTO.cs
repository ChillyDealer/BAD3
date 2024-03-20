using BAD3.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
    public class DriverDTO
    {
        [Required]
        public int DriverId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}