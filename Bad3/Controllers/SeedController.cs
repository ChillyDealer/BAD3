using System.Collections.ObjectModel;
using Bad3.Database;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bad3.Controllers;

[Route("[controller]")]
[ApiController]
public class SeedController : ControllerBase
{
    private BakeryDbContext _context;
    
    public SeedController(BakeryDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> SeedDb()
    {
        Customer cuh = new Customer
        {
            CustomerName = "Hej",
            Orders = new Collection<Order>()
        };
    
        if (!_context.Customer.Any(c => c.CustomerID == cuh.CustomerID))
        {
            await _context.Customer.AddAsync(cuh);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(SeedDb), new { id = cuh.CustomerID }, cuh);
        }
        else
        {
            return Conflict("Customer already exists.");
        }
    }
}