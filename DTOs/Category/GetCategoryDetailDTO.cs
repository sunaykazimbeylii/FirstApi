using TestApi.DTOs.Product;

namespace TestApi.DTOs.Category
{
    public record GetCategoryDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<GetProductDTO> ProductDTOs { get; set; }
    }
}
