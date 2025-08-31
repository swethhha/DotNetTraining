using AutoMapper;
using ShopTrackPro.Core.DTO;   // ✅ Corrected namespace
using ShopTrackPro.Core.Entities;
using ShopTrackPro.Core.Exceptions;
using ShopTrackPro.Core.Interfaces;

namespace ShopTrackPro.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepo, IMapper mapper)
        {
            _productRepo = productRepo;
            _mapper = mapper;
        }

        public async Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO productDto)
        {
            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ShopTrackPro.Core.Exceptions.ValidationException(
                    new Dictionary<string, string[]>
                    {
                        { "Name", new[] { "Product name is required." } }
                    });
            }

            if (productDto.Price < 0)
            {
                throw new ShopTrackPro.Core.Exceptions.ValidationException(
                    new Dictionary<string, string[]>
                    {
                        { "Price", new[] { "Price cannot be negative." } }
                    });
            }

            var product = _mapper.Map<Product>(productDto);

            await _productRepo.AddAsync(product);
            await _productRepo.SaveChangesAsync();

            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task<ProductResponseDTO> UpdateProductAsync(int id, ProductRequestDTO productDto)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }

            if (string.IsNullOrWhiteSpace(productDto.Name))
            {
                throw new ShopTrackPro.Core.Exceptions.ValidationException(
                    new Dictionary<string, string[]>
                    {
                        { "Name", new[] { "Product name is required." } }
                    });
            }

            if (productDto.Price < 0)
            {
                throw new ShopTrackPro.Core.Exceptions.ValidationException(
                    new Dictionary<string, string[]>
                    {
                        { "Price", new[] { "Price cannot be negative." } }
                    });
            }

            _mapper.Map(productDto, product);

            _productRepo.Update(product);
            await _productRepo.SaveChangesAsync();

            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }

            _productRepo.Delete(product);
            await _productRepo.SaveChangesAsync();

            return true;
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepo.GetByIdAsync(id);
            if (product == null)
            {
                throw new NotFoundException($"Product with ID {id} not found.");
            }

            return _mapper.Map<ProductResponseDTO>(product);
        }

        public async Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync()
        {
            var products = await _productRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductResponseDTO>>(products);
        }
    }
}
