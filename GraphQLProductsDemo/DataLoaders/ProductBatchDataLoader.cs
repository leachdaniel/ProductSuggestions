using GraphQLProductsDemo.Products;
using GraphQLProductsDemo.Repositories;
using GreenDonut;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GraphQLProductsDemo.DataLoaders
{
    public class ProductBatchDataLoader : BatchDataLoader<int, Product>
    {
        private readonly IProductsRepository _repository;

        public ProductBatchDataLoader(
            IProductsRepository repository,
            IBatchScheduler batchScheduler,
            DataLoaderOptions? options = null)
            : base(batchScheduler, options)
        {
            _repository = repository;
        }

        protected override async Task<IReadOnlyDictionary<int, Product>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            var values = await _repository.GetAsync(keys);
            return values.ToDictionary(x => x.ItemNumberId);
        }
    }
}
