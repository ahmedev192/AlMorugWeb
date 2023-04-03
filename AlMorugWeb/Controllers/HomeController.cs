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
        private readonly IProductRepository _productRepository = null;


        public HomeController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ViewResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var Data = await _productRepository.Search(searchString);
                ViewBag.SearchString = searchString;
                return View(Data);
            }

            else
            {
                //var data = await _productRepository.GetAll();
                return View();
            }

        }



        public async Task<IActionResult> Projects(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = sortOrder == "n_a" ? "n_d" : "n_a";
            ViewData["DateSortParm"] = sortOrder == "t_a" ? "t_d" : "t_a";
            ViewData["PriceParm"] = sortOrder == "p_d" ? "p_a" : "p_d";
            ViewData["PhoneParm"] = sortOrder == "ph_d" ? "ph_a" : "ph_d";
            ViewData["InternalParm"] = sortOrder == "i_d" ? "i_a" : "i_d";
            ViewData["DescParm"] = sortOrder == "d_d" ? "d_a" : "d_d";

            ViewBag.SearchString = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                var Data = await _productRepository.Search(searchString);
                var model = _productRepository.Sort(Data, sortOrder);
                return View(model);
            }

            else
            {
                var data = await _productRepository.GetAll(true);
                var model = _productRepository.Sort(data, sortOrder);

                return View(model);
            }
        }
        public async Task<IActionResult> RealState(string searchString, string sortOrder)
        {
            ViewData["NameSortParm"] = sortOrder == "n_a" ? "n_d" : "n_a";
            ViewData["DateSortParm"] = sortOrder == "t_a" ? "t_d" : "t_a";
            ViewData["PriceParm"] = sortOrder == "p_d" ? "p_a" : "p_d";
            ViewData["PhoneParm"] = sortOrder == "ph_d" ? "ph_a" : "ph_d";
            ViewData["InternalParm"] = sortOrder == "i_d" ? "i_a" : "i_d";
            ViewData["DescParm"] = sortOrder == "d_d" ? "d_a" : "d_d";

            ViewBag.SearchString = searchString;
            if (!String.IsNullOrEmpty(searchString))
            {
                ViewBag.SearchString = searchString;
                var Data = await _productRepository.Search(searchString);
                var model = _productRepository.Sort(Data, sortOrder);
                return View(model);
            }

            else
            {
                var data = await _productRepository.GetAll(false);
                var model = _productRepository.Sort(data, sortOrder);

                return View(model);
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}