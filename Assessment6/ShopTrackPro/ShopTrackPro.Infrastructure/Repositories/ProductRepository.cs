using Microsoft.EntityFrameworkCore;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Interfaces;
using ShopTrackPro.Infrastructure.Data;

namespace ShopTrackPro.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopTrackProContext _context;

        public ProductRepository(ShopTrackProContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Product entity)
        {
            await _context.Products.AddAsync(entity);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Update(Product entity)
        {
            _context.Products.Update(entity);
        }

        public void Delete(Product entity)
        {
            _context.Products.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
