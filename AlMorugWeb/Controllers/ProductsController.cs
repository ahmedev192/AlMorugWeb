﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AlMorugWeb.Data;
using AlMorugWeb.Models;
using AlMorugWeb.Repository;
using AlMorugWeb.Models.ViewModels;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.IO;

namespace AlMorugWeb.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;



        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IProductRepository productRepository)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _productRepository = productRepository;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string sortOrder)
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
                var data = await _productRepository.GetAllAdmin();
                var model = _productRepository.Sort(data, sortOrder);

                return View(model);
            }
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var data = await _productRepository.GetProductById(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }


        // GET: Products/Create
        public IActionResult Create(bool isSuccess = false, int productId = 0)
        {
            var model = new ProductModel();

            ViewBag.IsSuccess = isSuccess;
            ViewBag.ProductId = productId;
            return View(model);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public async Task<IActionResult> Create(/*[Bind("Id")]*/ ProductModel productModel)
        {
            if (ModelState.IsValid)
            {
                if (productModel.GalleryFiles != null)
                {
                    string folder = "uploads/";

                    productModel.Gallery = new List<GalleryModel>();


                    foreach (var file in productModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,

                            URL = UploadImage(folder, file)
                        };





                        productModel.Gallery.Add(gallery);
                    }
                }


                int id = await _productRepository.AddNewBook(productModel);
                if (id > 0)
                {
                    return RedirectToAction(nameof(Index), "Home");
                }
            }

            return View();
        }



        //GET
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var data = await _productRepository.GetProductById(id);

            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public IActionResult Edit(ProductModel obj)
        {

            if (ModelState.IsValid)
            {
                if (obj.GalleryFiles != null)
                {
                    string folder = "uploads/";

                    obj.Gallery = new List<GalleryModel>();

                    foreach (var file in obj.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = UploadImage(folder, file)
                        };
                        obj.Gallery.Add(gallery);
                    }
                }



                _productRepository.Update(obj);
                return RedirectToAction("Index", "Home");
            }
            return View(obj);
        }







        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Products'  is null.");
            }
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var product = await _context.Products.FindAsync(id);
            var prod = await _productRepository.GetProductById(id);
            string[] urls = new string[prod.Gallery.Count()];
            for (int i = 0; i < prod.Gallery.Count(); i++)
            {
                urls[i] = prod.Gallery[i].URL;
                var upload = wwwRootPath + urls[i];
                if (System.IO.File.Exists(upload))
                {
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(upload);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\tMessage: {ex.Message}");

                    }
                }

            }


            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        private string UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            //.
            string path = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);
            Image image = Image.FromStream(file.OpenReadStream(), true, true);
            var newImage = new Bitmap(700, 600);
            using (var a = Graphics.FromImage(newImage))
            {
                a.DrawImage(image, 0, 0, 700, 600);
                newImage.Save(path);
            }

            //.

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            //await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return "/" + folderPath;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearOldPhotos(int Id)
        {

            string wwwRootPath = _webHostEnvironment.WebRootPath;
            var prod = await _productRepository.GetProductById(Id);
            string[] urls = new string[prod.Gallery.Count()];
            for (int i = 0; i < prod.Gallery.Count(); i++)
            {
                urls[i] = prod.Gallery[i].URL;
                var upload = wwwRootPath + urls[i];
                if (System.IO.File.Exists(upload))
                {
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        System.IO.File.Delete(upload);

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"\tMessage: {ex.Message}");

                    }
                }

            }
            var x = _context.ProductGallery.Where(u => u.ProductId == Id).ToList();
            _context.ProductGallery.RemoveRange(x);
            //_context.ProductGallery.Remove();

            //_context.ProductGallery.RemoveRange(_context.ProductGallery.Where(u => u.ProductId == Id).ToList());

            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = Id });

        }
    }
}
