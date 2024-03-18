using Bad3.Database;
using Bad3.Model;
using Microsoft.AspNetCore.Mvc;

namespace Bad3.Controllers;

[Route("[controller]")]
[ApiController]
public class BakeryController : ControllerBase
{
    private BakeryDbContext _context;
    
    public BakeryController(BakeryDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<Order> Get()
    {
        return new Order(){CustomerId = 123};
    }
}