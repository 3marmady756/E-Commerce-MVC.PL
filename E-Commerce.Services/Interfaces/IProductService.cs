using E_Commerce.Data.DataOrEntity;
using E_Commerce.Services.Model_View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E_Commerce.Services.Interfaces
{
     public interface IProductService
    {
        Task<IEnumerable<Product>> GetAllProductsAsync();

        Task<Product?> GetProductByIdAsync(int? id);
        
        Task AddProductAsync(ProductVM productVm);

        Task UpdateProductAsync(UpdateProductVM updateProductVM);

        Task DeleteProductAsync(int? id);

        Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetAllTypesAsync();
        
        //Task<Product?> GetProductByNameAsync(string name);
    }
}
