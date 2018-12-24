using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.ServicesHosting.Controllers
{
    [Produces("application/json")]
    [Route("api/products")]
    public class ProductsApiController : Controller, IProductData
    {
        private readonly IProductData _productData;

        public ProductsApiController(IProductData productData)
        {
            _productData = productData;
        }

        // GET api/products/sections
        [HttpGet("sections")]
        public IEnumerable<SectionDto> GetSections()
        {
            return _productData.GetSections();
        }

        // GET api/products/sections/{id}
        [HttpGet("sections/{id}")]
        public SectionDto GetSectionById(int id)
        {
            return _productData.GetSectionById(id);
        }

        // GET api/products/brands
        [HttpGet("brands")]
        public IEnumerable<BrandDto> GetBrands()
        {
            return _productData.GetBrands();
        }

        // GET api/products/brands/{id}
        [HttpGet("brands/{id}")]
        public BrandDto GetBrandById(int id)
        {
            return _productData.GetBrandById(id);
        }

        // POST api/products
        [HttpPost]
        public IEnumerable<ProductDto> GetProducts([FromBody]ProductFilter filter)
        {
            return _productData.GetProducts(filter);
        }

        // GET api/products/{id}
        [HttpGet("{id}")]
        public ProductDto GetProductById(int id)
        {
            return _productData.GetProductById(id);
        }
        
    }
}