using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using WebStore.Domain.Filters;
using WebStore.Domain.ViewModel;
using WebStore.Domain.ViewModel.Product;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CatalogController : Controller
    {
        private IProductData _productData;
        private readonly IConfiguration _configuration;

        public CatalogController(IProductData productData, IConfiguration configuration)
        {
            _productData = productData;
            _configuration = configuration;
        }

        public IActionResult Shop(int? sectionId, int? brandId, int page = 1)
        {
            int.TryParse(_configuration["PageSize"], out int pageSize);
            var products = _productData.GetProducts(
                new ProductFilter
                {
                    BrandId = brandId,
                    SectionId = sectionId,
                    Page = page,
                    PageSize = pageSize
                }
            );

            var sections = _productData.GetSections();
            var brands = _productData.GetBrands();
            var title = "Товары";
            if (sectionId.HasValue)
            {
                title = sections.FirstOrDefault(c => c.Id == sectionId)?.Name;
            }
            else if (brandId.HasValue)
            {
                title = brands.FirstOrDefault(c => c.Id == brandId)?.Name;
            }

            var model = new CatalogViewModel()
            {
                BrandId = brandId,
                SectionId = sectionId,
                ProductsViewModel = new ProductsViewModel()
                {
                    Title = title,
                    Products = products.Products.Select(p =>
                        new ProductItemViewModel()
                        {
                            Id = p.Id,
                            ImageUrl = p.ImageUrl,
                            Name = p.Name,
                            Order = p.Order,
                            Price = p.Price,
                            Brand = p.Brand != null ? p.Brand.Name : string.Empty
                        }
                    ).OrderBy(p => p.Order).ToList(),
                    PageViewModel = new PageViewModel
                    {
                        PageSize = pageSize,
                        PageNumber = page,
                        TotalItems = products.TotalCount
                    }
                }
            };

            return View(model);
        }

        public IActionResult ProductDetails(int id)
        {
            var product = _productData.GetProductById(id);
            if (product == null)
                return NotFound();

            return View(new ProductItemViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price,
                Brand = product.Brand != null ? product.Brand.Name : string.Empty
            });

        }
    }
}