using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using OrderWebApi.Model;

namespace OrderWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMongoCollection<Order> _mongoCollection;
        public OrderController()
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_Name");
            var connectionString = $"mongodb://{dbHost}:27017/{dbName}";

            var mongoUrl = MongoUrl.Create(connectionString);
            var mongoClient = new MongoClient(mongoUrl);
            var database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
            _mongoCollection = database.GetCollection<Order>("order");

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> Get()
        {
            return await _mongoCollection.Find(Builders<Order>.Filter.Empty).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetById(string id)
        {
            var filterDefination = Builders<Order>.Filter.Eq(x=>x.Id, id);
            return await _mongoCollection.Find(filterDefination).SingleOrDefaultAsync();
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] Order order)
        {
            await _mongoCollection.InsertOneAsync(order);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] Order order)
        {
            var filterDefination = Builders<Order>.Filter.Eq(x=>x.Id, order.Id);
            await _mongoCollection.ReplaceOneAsync(filterDefination, order);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var filterDefination = Builders<Order>.Filter.Eq(x => x.Id, id);
            _mongoCollection.DeleteOneAsync(filterDefination);

            return Ok();
        }
    }
}
