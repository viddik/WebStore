using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Services.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        protected sealed override string ServiceAddress { get; set; }

        public ProductsClient(IConfiguration configuration) : base(configuration)
        {
            ServiceAddress = "api/products";
        }

        public IEnumerable<SectionDto> GetSections()
        {
            var url = $"{ServiceAddress}/sections";
            return Get<List<SectionDto>>(url);
        }

        public SectionDto GetSectionById(int id)
        {
            var url = $"{ServiceAddress}/sections/{id}";
            var result = Get<SectionDto>(url);
            return result;
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            var url = $"{ServiceAddress}/brands";
            return Get<List<BrandDto>>(url);
        }

        public BrandDto GetBrandById(int id)
        {
            var url = $"{ServiceAddress}/brands/{id}";
            var result = Get<BrandDto>(url);
            return result;
        }

        public IEnumerable<ProductDto> GetProducts(ProductFilter filter)
        {
            var url = $"{ServiceAddress}";
            var response = Post(url, filter);
            return response.Content.ReadAsAsync<IEnumerable<ProductDto>>().Result;
        }

        public ProductDto GetProductById(int id)
        {
            var url = $"{ServiceAddress}/{id}";
            return Get<ProductDto>(url);
        }
    }
}
