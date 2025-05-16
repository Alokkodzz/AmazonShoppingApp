using ProductService.Models;

namespace ProductService.Services
{
    public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<Product> AddProductAsync(Product product);
    }
}
