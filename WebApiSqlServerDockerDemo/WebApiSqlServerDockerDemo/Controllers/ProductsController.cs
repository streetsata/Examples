using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiSqlServerDockerDemo.Data;
using WebApiSqlServerDockerDemo.Models;

namespace WebApiSqlServerDockerDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(OnlineShopDbContext context) : ControllerBase
    {
        private readonly OnlineShopDbContext _context = context;

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
