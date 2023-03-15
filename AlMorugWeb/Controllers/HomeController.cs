using AlMorugWeb.Models;
using AlMorugWeb.Models.ViewModels;
using AlMorugWeb.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AlMorugWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductRepository _productRepository = null;


        public HomeController(ILogger<HomeController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<ViewResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var Data = await _productRepository.Search(searchString);
                return View(Data);
            }

            else
            {
                var data = await _productRepository.GetAll();
                return View(data);
            }
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}