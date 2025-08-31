using ShopTrackPro.Core.DTO;
using ShopTrackPro.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTrackPro.Core.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDTO>> GetAllProductsAsync();
        Task<ProductResponseDTO?> GetProductByIdAsync(int id);
        Task<ProductResponseDTO> CreateProductAsync(ProductRequestDTO product);
        Task<ProductResponseDTO> UpdateProductAsync(int id, ProductRequestDTO product);
        Task<bool> DeleteProductAsync(int id);
    }
}
