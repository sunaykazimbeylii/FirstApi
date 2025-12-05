namespace TestApi.Repositories.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        IQueryable<Product> GetAllImage();
    }
}
