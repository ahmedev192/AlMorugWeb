using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AlMorugWeb.Models.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsInternal { get; set; }

        [Display(Name = "Choose the gallery images of your book")]
        [ValidateNever]
        public IFormFileCollection GalleryFiles { get; set; }
        [ValidateNever]
        public List<GalleryModel> Gallery { get; set; }
    }
}
