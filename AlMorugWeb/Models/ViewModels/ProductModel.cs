using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AlMorugWeb.Models.ViewModels
{
    public class ProductModel
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string ProductNameAr { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DescriptionAr { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsInternal { get; set; }
        public string Location { get; set; } = string.Empty;
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        public int Price { get; set; }

        [Display(Name = "Images")]
        [ValidateNever]
        public IFormFileCollection? GalleryFiles { get; set; }
        [ValidateNever]
        public List<GalleryModel> Gallery { get; set; }  = new List<GalleryModel>();
    }
}
