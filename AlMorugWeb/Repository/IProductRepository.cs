﻿using AlMorugWeb.Models.ViewModels;

namespace AlMorugWeb.Repository
{
    public interface IProductRepository
    {
        Task<int> AddNewBook(ProductModel model);
        Task<ProductModel> GetProductById(int id);
        Task<List<ProductModel>> GetAll();
        void Update(ProductModel model);
        Task<List<ProductModel>> Search(string searchString);




    }
}
