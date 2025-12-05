using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context) { }

        public IQueryable<Product> GetAllImage()
        {
            throw new NotImplementedException();
        }
    }
}
