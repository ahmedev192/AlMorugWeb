﻿using AlMorugWeb.Models.ViewModels;

namespace AlMorugWeb.Repository
{
    public interface IProductRepository
    {
        Task<int> AddNewBook(ProductModel model);
        Task<ProductModel?> GetProductById(int id);
        Task<List<ProductModel>> GetAll(bool intern);
        Task<List<ProductModel>> GetAllAdmin();
        void Update(ProductModel model);
        List<ProductModel> Sort(List<ProductModel> data, string? orderby = null);

        Task<List<ProductModel>> Search(string searchString);

        //Models.ViewModels.Index index();




    }
}
