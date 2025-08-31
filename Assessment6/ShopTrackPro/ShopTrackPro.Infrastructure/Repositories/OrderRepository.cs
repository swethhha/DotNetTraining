using Microsoft.EntityFrameworkCore;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Interfaces;
using ShopTrackPro.Infrastructure.Data;

namespace ShopTrackPro.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopTrackProContext _context;

        public OrderRepository(ShopTrackProContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
       .Include(o => o.OrderItems)
           .ThenInclude(oi => oi.Product)
       .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                 .ThenInclude(oi => oi.Product) // include product for price/name
                    .FirstOrDefaultAsync(o => o.Id == id);

        }

        public void Update(Order entity)
        {
            _context.Orders.Update(entity);
        }

        public void Delete(Order entity)
        {
            _context.Orders.Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
