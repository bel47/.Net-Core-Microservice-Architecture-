using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductWebApi.Model;

namespace ProductWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductDbContext _productDbContext;

        public ProductController(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get()
        {
            return _productDbContext.Products;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var customer = await _productDbContext.Products.FindAsync(id);

            return customer;
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Product customer)
        {
            await _productDbContext.Products.AddAsync(customer);
            await _productDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Product customer)
        {
            _productDbContext.Products.Update(customer);
            await _productDbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var customer = await _productDbContext.Products.FindAsync(id);
            _productDbContext.Remove(customer);
            await _productDbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
