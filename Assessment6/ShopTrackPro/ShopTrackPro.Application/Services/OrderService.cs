using AutoMapper;
using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepo, IUserRepository userRepo, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<OrderResponseDTO> CreateOrderAsync(OrderRequestDTO dto)
        {
            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "UserId", new[] { "Invalid UserId. User does not exist." } }
                });

            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                OrderItems = dto.Items?.Select(i => new OrderItem
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList() ?? new List<OrderItem>()
            };

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();

            // Reload the order including OrderItems and Products
            var createdOrder = await _orderRepo.GetByIdAsync(order.Id);
            return _mapper.Map<OrderResponseDTO>(createdOrder);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<IEnumerable<OrderResponseDTO>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = (await _orderRepo.GetAllAsync())
                         .Where(o => o.UserId == userId);
            return _mapper.Map<IEnumerable<OrderResponseDTO>>(orders);
        }

        public async Task<OrderResponseDTO> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) throw new NotFoundException($"Order with ID {id} not found.");
            return _mapper.Map<OrderResponseDTO>(order);
        }

        public async Task UpdateOrderAsync(int id, OrderRequestDTO dto)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) throw new NotFoundException($"Order with ID {id} not found.");

            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null)
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { "UserId", new[] { "Invalid UserId. User does not exist." } }
                });

            order.UserId = dto.UserId;

            // Replace OrderItems
            order.OrderItems = dto.Items?.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity = i.Quantity
            }).ToList() ?? new List<OrderItem>();

            _orderRepo.Update(order);
            await _orderRepo.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) throw new NotFoundException($"Order with ID {id} not found.");

            _orderRepo.Delete(order);
            await _orderRepo.SaveChangesAsync();
        }
    }
}
