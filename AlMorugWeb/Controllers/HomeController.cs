using AlMorugWeb.Models;
using AlMorugWeb.Models.ViewModels;
using AlMorugWeb.Repository;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Diagnostics;
using Index = AlMorugWeb.Models.ViewModels.Index;

namespace AlMorugWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IStringLocalizer<HomeController> _localizer;


        public HomeController(IProductRepository productRepository, IStringLocalizer<HomeController> localizer)
        {
            _productRepository = productRepository;
            _localizer = localizer;
        }

        public async Task<ViewResult> Index()
        {
            var data1 = await _productRepository.GetAll(true);
            var data2 = await _productRepository.GetAll(false);

            var all = new Index()
            {
                Products = data1,
                Realstates = data2,
            };

            //var data = await _productRepository.GetAll();
            return View(all); ;

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
        //.

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );

            return LocalRedirect(returnUrl);
        }
        //.
    }
}