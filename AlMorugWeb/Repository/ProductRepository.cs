using AlMorugWeb.Data;
using AlMorugWeb.Models;
using AlMorugWeb.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Reflection.Metadata.BlobBuilder;

namespace AlMorugWeb.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context = null;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(ProductModel model)
        {
            var newProduct = new Product()
            {
                PhoneNumber = model.PhoneNumber,
                ProductName = model.ProductName,
                Description = model.Description,
                IsInternal = model.IsInternal,
            };

            newProduct.productGallery = new List<ProductGallery>();

            foreach (var file in model.Gallery)
            {
                newProduct.productGallery.Add(new ProductGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }

            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.Id;

        }

        public async Task<List<ProductModel>> GetAll()
        {
            return await _context.Products
                  .Select(product => new ProductModel()
                  {
                      ProductName = product.ProductName,
                      PhoneNumber= product.PhoneNumber,
                      Description= product.Description,
                      IsInternal = product.IsInternal,
                      Gallery = product.productGallery.Select(g => new GalleryModel()
                      {
                          Id = g.Id,
                          Name = g.Name,
                          URL = g.URL
                      }).ToList()
                  }).ToListAsync();
        }


        public async Task<ProductModel> GetProductById(int id)
        {
            return await _context.Products.Where(x => x.Id == id)
                 .Select(product => new ProductModel()
                 {
                     Id = product.Id,
                     PhoneNumber = product.PhoneNumber,
                     ProductName = product.ProductName,
                     Description = product.Description,
                     IsInternal = product.IsInternal,
                     Gallery = product.productGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList()

                 }).FirstOrDefaultAsync();
        }




    }
}
