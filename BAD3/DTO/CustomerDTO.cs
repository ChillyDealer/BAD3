using BAD3.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Bad3.DTO
{
    public class CustomerDTO
    {
        [Required]
        public string CustomerName { get; set; }
    }
}