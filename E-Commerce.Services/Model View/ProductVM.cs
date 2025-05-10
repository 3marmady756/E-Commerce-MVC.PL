using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services.Model_View
{
    public class ProductVM
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public decimal Price { get; set; }

        public string? Description { get; set; }

        public int StockQuantity { get; set; }

        public IFormFile Image { get; set; }

        public string? BrandName { get; set; }

        public string? TypeName { get; set; }
         
        public int ProductBrandId { get; set; }

        public int ProductTypeId { get; set; }
    }
}
