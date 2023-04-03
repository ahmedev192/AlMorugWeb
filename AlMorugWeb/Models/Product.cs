using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AlMorugWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsInternal { get; set; }
        public string Location { get; set; } = string.Empty;
        public int Price { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [ValidateNever]
        public ICollection<ProductGallery> productGallery { get; set; } = new List<ProductGallery>();

    }
}
