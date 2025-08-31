using Microsoft.EntityFrameworkCore;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Interfaces;
using ShopTrackPro.Infrastructure.Data;

namespace ShopTrackPro.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly ShopTrackProContext _context;

        public OrderItemRepository(ShopTrackProContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                                 .Include(oi => oi.Order)
                                 .Include(oi => oi.Product)
                                 .ToListAsync();
        }

        public async Task<OrderItem?> GetByIdAsync(int id)
        {
            return await _context.OrderItems
                                 .Include(oi => oi.Order)
                                 .Include(oi => oi.Product)
                                 .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public void Update(OrderItem entity)
        {
            _context.OrderItems.Update(entity);
        }

        public void Delete(OrderItem entity)
        {
            _context.OrderItems.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
