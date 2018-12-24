using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Context;
using WebStore.Domain.Dto.Product;
using WebStore.Domain.Entities;
using WebStore.Domain.Filters;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Sql
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreContext _context;

        public SqlProductData(WebStoreContext context)
        {
            _context = context;
        }

        public IEnumerable<SectionDto> GetSections()
        {
            return _context.Sections.Select(s => new SectionDto()
            {
                Id = s.Id,
                Name = s.Name,
                Order = s.Order,
                ParentId = s.ParentId
            }).ToList();
        }

        public SectionDto GetSectionById(int id)
        {
            SectionDto result = null;
            var section = _context.Sections.FirstOrDefault(s => s.Id == id);
            if (section != null)
            {
                result = new SectionDto
                {
                    Id = section.Id,
                    Name = section.Name,
                    Order = section.Order,
                    ParentId = section.ParentId
                };
            }
            return result;
        }

        public IEnumerable<BrandDto> GetBrands()
        {
            return _context.Brands.Select(b => new BrandDto()
            {
                Id = b.Id,
                Name = b.Name,
                Order = b.Order
            }).ToList();
        }

        public BrandDto GetBrandById(int id)
        {
            BrandDto result = null;
            var brand = _context.Brands.FirstOrDefault(s => s.Id == id);
            if (brand != null)
            {
                result = new BrandDto
                {
                    Id = brand.Id,
                    Name = brand.Name,
                    Order = brand.Order
                };
            }
            return result;
        }

        public PagedProductDto GetProducts(ProductFilter filter)
        {
            var query = _context.Products.Include("Brand").Include("Section").AsQueryable();
            
            // Фильтр по вхождению в список id
            if (filter.Ids?.Count > 0)
                query = query.Where(c => filter.Ids.Contains(c.Id));
            // Фильтр по бренду
            if (filter.BrandId.HasValue)
                query = query.Where(c => c.BrandId.HasValue && c.BrandId.Value.Equals(filter.BrandId.Value));
            // Фильтр по секции
            if (filter.SectionId.HasValue)
                query = query.Where(c => c.SectionId.Equals(filter.SectionId.Value));

            var model = new PagedProductDto
            {
                TotalCount = query.Count()
            };

            if (filter.PageSize.HasValue)
            {
                model.Products = query.OrderBy(c => c.Order)
                    .Skip((filter.Page - 1) * filter.PageSize.Value)
                    .Take(filter.PageSize.Value)
                    .Select(p => new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        Brand = p.BrandId.HasValue ? new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                        Section = new SectionDto() { Id = p.SectionId, Name = p.Section.Name }
                    }).ToList();
            }
            else
            {
                model.Products = query.OrderBy(c => c.Order)
                    .Select(p => new ProductDto()
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Order = p.Order,
                        Price = p.Price,
                        ImageUrl = p.ImageUrl,
                        Brand = p.BrandId.HasValue ? new BrandDto() { Id = p.Brand.Id, Name = p.Brand.Name } : null,
                        Section = new SectionDto() { Id = p.SectionId, Name = p.Section.Name }
                    }).ToList();
            }

            return model;
        }

        public ProductDto GetProductById(int id)
        {
            ProductDto result = null;
            var product = _context.Products.Include("Brand").Include("Section").FirstOrDefault(p => p.Id.Equals(id));
            if (product != null)
            {
                result = new ProductDto()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Order = product.Order,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Brand = product.BrandId.HasValue ? new BrandDto() { Id = product.Brand.Id, Name = product.Brand.Name } : null,
                    Section = new SectionDto() { Id = product.SectionId, Name = product.Section.Name }
                };
            }
            return result;
        }
    }
}
