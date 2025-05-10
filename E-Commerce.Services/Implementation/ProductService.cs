using Azure;
using E_Commerce.Data.DataOrEntity;
using E_Commerce.Services.FormFiles;
using E_Commerce.Services.Interfaces;
using E_Commerce.Services.Model_View;
using E_Commerce_.Repository.Interfaces;
using E_Commerce_.Repository.Repository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace E_Commerce.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
     

        public ProductService(IUnitOfWork unitOfWork,IFileService fileService) 
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            
            
        }
       

        public async Task<IReadOnlyList<ProductBrand>> GetAllBrandsAsync()
        {
            return await _unitOfWork.ProductRepository.GetProductBrandsAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllWithBrandAndTypeAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllTypesAsync()
        {
            return await _unitOfWork.ProductRepository.GetProductTypesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int? id)
        {
           if(id<=0)
                throw new ArgumentOutOfRangeException("Id Is Not Valid");
            return await _unitOfWork.ProductRepository.GetByIdAsync(id.Value);
        }

        public  async Task UpdateProductAsync(UpdateProductVM updateProductVM)
        {
            if (updateProductVM.Id == null || updateProductVM.Id <= 0)
                throw new ArgumentOutOfRangeException("Invalid Product Id");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(updateProductVM.Id);

            if (product == null)
                throw new ArgumentException("Product not found");

            // تحديث الخصائص (حسب خصائص ProductVM و Product)
            product.Name = updateProductVM.Name;
            product.Description = updateProductVM.Description;
            product.Price = updateProductVM .Price;
            product.StockQuantity = updateProductVM.StockQuantity;
            
           

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.CompleteAsync();
        }

        #region Add product
        public async Task AddProductAsync(ProductVM productVm)
        {
            if (productVm == null)
                throw new ArgumentNullException("VM is Null");
                

            if (string.IsNullOrWhiteSpace(productVm.Name))
                throw new ArgumentNullException("Product Name is Required");

            if (productVm.Price == null || productVm.Price <= 0)
                throw new ArgumentOutOfRangeException("Product price must be greater than 0");

            if (productVm.ProductBrandId == null || productVm.ProductBrandId <= 0)
                throw new ArgumentOutOfRangeException("InValid Brand ID is required");



            if (productVm.ProductTypeId == null || productVm.ProductTypeId <= 0)
                throw new ArgumentOutOfRangeException("InValid Type ID is required");

            string? imageUrl = null;

            if (productVm.Image != null)
            {
                imageUrl = await _fileService.UploadFileAsync(productVm.Image);

                if (imageUrl == null)
                    throw new ArgumentOutOfRangeException("Upload Image Failed");
            }

            var product = new Product
            {
                Name = productVm.Name,
                Description = productVm.Description,
                Price = productVm.Price,
                ProductBrandId = productVm.ProductBrandId,
                ProductTypeId = productVm.ProductTypeId,
                PictureUrl = imageUrl
            };

            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            
        }
        #endregion

        public async Task DeleteProductAsync(int? id)
        {
            if (id == null || id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id), "Invalid Product Id");

            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id.Value);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {id} was not found.");

            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.CompleteAsync();
        }
    }
}
