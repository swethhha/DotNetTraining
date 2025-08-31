using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.Application.Services
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepo;
        private readonly IOrderRepository _orderRepo;
        private readonly IProductRepository _productRepo;

        public OrderItemService(
            IOrderItemRepository orderItemRepo,
            IOrderRepository orderRepo,
            IProductRepository productRepo)
        {
            _orderItemRepo = orderItemRepo;
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        /// <summary>
        /// Adds a product to the cart (order). If it already exists, increases quantity.
        /// </summary>
        public async Task<OrderItemResponseDTO> AddOrderItemAsync(int orderId, OrderItemRequestDTO dto)
        {
            if (dto.Quantity <= 0)
            {
                throw new ValidationException(new Dictionary<string, string[]>
                {
                    { nameof(dto.Quantity), new[] { "Quantity must be greater than zero." } }
                });
            }

            // Ensure order exists
            var order = await _orderRepo.GetByIdAsync(orderId)
                        ?? throw new NotFoundException($"Order (cart) with ID {orderId} not found.");

            // Example: only allow certain roles to add items (illustrative)
            // if (!UserIsAuthorizedToModifyOrder(orderId)) 
            //     throw new ForbiddenException("You do not have permission to add items to this order.");

            // Ensure product exists
            var product = await _productRepo.GetByIdAsync(dto.ProductId)
                          ?? throw new NotFoundException($"Product with ID {dto.ProductId} not found.");

            // Check if item already exists in cart
            var existingItem = (await _orderItemRepo.GetAllAsync())
                                .FirstOrDefault(i => i.OrderId == orderId && i.ProductId == dto.ProductId);

            if (existingItem != null)
            {
                // Example conflict scenario
                if (existingItem.Quantity + dto.Quantity > 100) // max allowed quantity
                    throw new ConflictException("Cannot add more than 100 units of this product.");

                existingItem.Quantity += dto.Quantity;
                _orderItemRepo.Update(existingItem);
            }
            else
            {
                var newItem = new OrderItem
                {
                    OrderId = orderId,
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity
                };
                await _orderItemRepo.AddAsync(newItem);
            }

            await _orderItemRepo.SaveChangesAsync();

            return new OrderItemResponseDTO
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = existingItem?.Quantity ?? dto.Quantity
            };
        }

        /// <summary>
        /// Returns all items in a cart (order).
        /// </summary>
        public async Task<IEnumerable<OrderItemResponseDTO>> GetOrderItemsByOrderIdAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId)
                        ?? throw new NotFoundException($"Order with ID {orderId} not found.");

            // Optional: unauthorized/forbidden checks for user roles
            // if (!UserIsAuthorizedToViewOrder(orderId)) 
            //     throw new UnauthorizedException("You are not authorized to view this order.");

            var items = (await _orderItemRepo.GetAllAsync())
                        .Where(i => i.OrderId == orderId)
                        .ToList();

            if (!items.Any())
                return Enumerable.Empty<OrderItemResponseDTO>();

            var result = new List<OrderItemResponseDTO>();

            foreach (var item in items)
            {
                var product = item.Product ?? await _productRepo.GetByIdAsync(item.ProductId)
                              ?? throw new NotFoundException($"Product with ID {item.ProductId} not found.");

                result.Add(new OrderItemResponseDTO
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = item.Quantity
                });
            }

            return result;
        }

        /// <summary>
        /// Removes a single item from the cart.
        /// </summary>
        public async Task DeleteOrderItemAsync(int id)
        {
            var item = await _orderItemRepo.GetByIdAsync(id)
                       ?? throw new NotFoundException($"Order item with ID {id} not found.");

            // Example: forbidden deletion scenario
            // if (!UserIsAuthorizedToDeleteItem(item))
            //     throw new ForbiddenException("You cannot delete this order item.");

            _orderItemRepo.Delete(item);
            await _orderItemRepo.SaveChangesAsync();
        }
    }
}
