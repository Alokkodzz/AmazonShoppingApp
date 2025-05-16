using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.Models;

namespace ProductService.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _db;
        public ProductService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> AddProductAsync(Product product)
        {
            _db.Products.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }
    }
}
