using GraphQLProductsDemo.Products;
using GreenDonut;
using HotChocolate.Execution;
using HotChocolate.Fetching;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GraphQLProductsDemo
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IRequestExecutorResolver _requestExecutorResolver;
        private readonly IDataLoader<int, Product> _batchDataLoader;
        private readonly IBatchDispatcher _batchScheduler;

        public ValuesController(IRequestExecutorResolver requestExecutorResolver, IDataLoader<int, Product> batchDataLoader, IBatchDispatcher batchScheduler)
        {
            _requestExecutorResolver = requestExecutorResolver;
            _batchDataLoader = batchDataLoader;
            _batchScheduler = batchScheduler;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var executor = await _requestExecutorResolver.GetRequestExecutorAsync();

            // the thing that I don't like is you need a query to define what you after
            // can you define a query in C#? no obvious way possibly strawberry shake
            //var results = await executor.ExecuteAsync("{ byItemNumberId(itemNumberId: 13) { price name }}");

            //_batchScheduler.DispatchOnSchedule = true;
            var tasks = new List<Task>();

            tasks.Add(_batchDataLoader.LoadAsync(1));
            tasks.Add(_batchDataLoader.LoadAsync(13));
            tasks.Add(_batchDataLoader.LoadAsync(122));
            var multi = _batchDataLoader.LoadAsync(150, 160, 160);
            tasks.Add(multi);

            _batchScheduler.BeginDispatch();

            tasks.Add(_batchDataLoader.LoadAsync(122));

            //_batchScheduler.BeginDispatch();

            await Task.WhenAll(tasks);
            var results = tasks.OfType<Task<Product>>().Select(t => t.Result);
            results = results.Concat(multi.Result);

            return Ok(results);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
