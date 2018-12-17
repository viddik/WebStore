using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;
using WebStore.Models;

namespace WebStore.ViewComponents
{
    public class BrandsViewComponent : ViewComponent
    {
        private readonly IProductData _productData;

        public BrandsViewComponent(IProductData productData)
        {
            _productData = productData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var brands = GetBrands();
            return View(brands);
        }

        private IEnumerable<BrandViewModel> GetBrands()
        {
            var dbBrands = _productData.GetBrands();
            var dbProducts = _productData.GetProducts(new ProductFilter());
            return dbBrands.Select(b => new BrandViewModel
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order,
                ProductsCount = dbProducts.Count(p => p.Brand.Id.Equals(b.Id))
            }).OrderBy(b => b.Order).ToList();
        }
    }
}
