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
        var order = new Order
        {
            OrderDate = DateTime.Now
        };
        
        var customer = new Customer
        {
            CustomerName = "Hej",
            Orders = new Collection<Order>()
            {
                order
            }
        };

        var good = new Goods
        {
            GoodName = "Sugar",
            Validity = new DateTime(2024, 8, 18),
            Quantity = 40
        };

        var goodsOrder = new GoodsOrder
        {
            GoodsId = 3,
            OrderId = 1,
            Quantity = 20
        };
        
        await _context.Goods.AddAsync(good);
        await _context.GoodsOrder.AddAsync(goodsOrder);
        await _context.SaveChangesAsync();
    
        if (!_context.Customer.Any(c => c.CustomerID == customer.CustomerID))
        {
            await _context.Customer.AddAsync(customer);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(SeedDb), new { id = customer.CustomerID }, customer);
        }
        else
        {
            return Conflict("Customer already exists.");
        }
    }
}