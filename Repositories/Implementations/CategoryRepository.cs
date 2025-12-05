using TestApi.Repositories.Interfaces;

namespace TestApi.Repositories.Implementations
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }

    }
}
