using Microsoft.EntityFrameworkCore;
using TestApi.DTOs.Category;
using TestApi.DTOs.Product;
using TestApi.Repositories.Interfaces;
using TestApi.Services.Interfaces;

namespace TestApi.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<bool> CreateAsync(CreateCategoryDTO categoryDTO)
        {
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name)) return false;
            await _categoryRepository.AddAsync(new Category { Name = categoryDTO.Name });
            await _categoryRepository.SaveChangesAsync();


            return true;
        }


        public async Task<IEnumerable<GetCategoryDTO>> GetAllAsync(int page, int take)
        {
            IEnumerable<GetCategoryDTO> categories = await _categoryRepository.GetAll(skip: (page - 1) * take, take: take).Select(c => new GetCategoryDTO
            {
                Id = c.Id,
                Name = c.Name,
                ProductCount = c.Products.Count

            }).ToListAsync();
            return categories;
        }


        async Task<GetCategoryDetailDTO> ICategoryService.GetByIdAsync(int id)
        {
            Category category = await _categoryRepository.GetById(id, nameof(Category.Products));
            if (category == null) return null;

            GetCategoryDetailDTO categoryDTO = new()
            {
                Id = category.Id,
                Name = category.Name,
                ProductDTOs = category.Products.Select(p => new GetProductDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                }).ToList()


            };
            return categoryDTO;
        }
        public async Task UpdateAsync(int id, UpdateCategoryDTO categoryDTO)
        {
            Category category = await _categoryRepository.GetById(id);
            if (category == null) throw new Exception("Not Found");
            if (await _categoryRepository.AnyAsync(c => c.Name == categoryDTO.Name && c.Id != id)) throw new Exception("Already exsist");
            category.Name = categoryDTO.Name;
            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();

        }
        public async Task DeleteAsync(int id)
        {
            Category category = await _categoryRepository.GetById(id);
            if (category == null) throw new Exception("not found");
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();



        }

    }
}
