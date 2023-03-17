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
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                ProductName = model.ProductName,
                Description = model.Description,
                IsInternal = model.IsInternal,
                Location = model.Location,
                CreatedDateTime = model.CreatedDateTime,
                Price = model.Price,
            };

            newProduct.productGallery = new List<ProductGallery>();
            if (model.Gallery != null)
            {
                foreach (var file in model.Gallery)
                {
                    newProduct.productGallery.Add(new ProductGallery()
                    {
                        Id = file.Id,
                        Name = file.Name,
                        URL = file.URL
                    });
                }

            }


            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return newProduct.Id;

        }





        public async void Update(ProductModel obj)
        {
            var objFromDb = _context.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.ProductName = obj.ProductName;
                objFromDb.PhoneNumber = obj.PhoneNumber;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.CreatedDateTime = obj.CreatedDateTime;
                objFromDb.IsInternal = obj.IsInternal;
                objFromDb.Location = obj.Location;

                objFromDb.productGallery = new List<ProductGallery>();

                if (obj.Gallery != null)
                {
                    foreach (var file in obj.Gallery)
                    {
                        objFromDb.productGallery.Add(new ProductGallery()
                        {
                            Id = file.Id,
                            Name = file.Name,
                            URL = file.URL
                        });
                    }
                }

                 _context.Products.Update(objFromDb);
                 _context.SaveChanges();
            }
        }




        public async Task<List<ProductModel>> GetAll()
        {
            return await _context.Products
                  .Select(product => new ProductModel()
                  {
                      Id = product.Id,
                      ProductName = product.ProductName,
                      PhoneNumber = product.PhoneNumber,
                      Description = product.Description,
                      IsInternal = product.IsInternal,
                      Location = product.Location,
                      CreatedDateTime = product.CreatedDateTime,
                      Price = product.Price,
                      Gallery = product.productGallery.Select(g => new GalleryModel()
                      {
                          Id = g.Id,
                          Name = g.Name,
                          URL = g.URL
                      }).ToList()
                  }).ToListAsync();
        }
        public async Task<List<ProductModel>> Search(string searchString)
        {
            return await _context.Products.Where(s => s.Description!.Contains(searchString) || s.ProductName!.Contains(searchString) ).Select(product => new ProductModel()
            {
                Id = product.Id,
                ProductName = product.ProductName,
                PhoneNumber = product.PhoneNumber,
                Description = product.Description,
                IsInternal = product.IsInternal,
                Location = product.Location,
                CreatedDateTime = product.CreatedDateTime,
                Price = product.Price,
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
                     Location = product.Location,
                     CreatedDateTime = product.CreatedDateTime,
                     Price = product.Price,
                     Gallery = product.productGallery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList()

                 }).FirstOrDefaultAsync();
        }


        public List<ProductModel> Sort(List<ProductModel> data, string? orderby = null)
        {
            switch (orderby)
            {
                case "n_a":
                    return data.OrderBy(data => data.ProductName).ToList();
                case "n_d":
                    return data.OrderByDescending(data => data.ProductName).ToList();
                case "p_a":
                    return data.OrderBy(data => data.Price).ToList();
                case "p_d":
                    return data.OrderByDescending(data => data.Price).ToList();
                case "t_a":
                    return data.OrderBy(data => data.CreatedDateTime).ToList();
                case "ph_d":
                    return data.OrderByDescending(data => data.PhoneNumber).ToList();
                case "ph_a":
                    return data.OrderBy(data => data.PhoneNumber).ToList();
                case "t_d":
                    return data.OrderByDescending(data => data.CreatedDateTime).ToList();
                case "i_a":
                    return data.OrderBy(data => data.IsInternal).ToList();
                case "i_d":
                    return data.OrderByDescending(data => data.IsInternal).ToList();
                case "d_a":
                    return data.OrderBy(data => data.Description).ToList();
                case "d_d":
                    return data.OrderByDescending(data => data.Description).ToList();



                default:
                    return data;

            }

        }

    }

}
