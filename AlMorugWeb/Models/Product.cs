using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace AlMorugWeb.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsInternal { get; set; }
        public string? Location { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
        [ValidateNever]
        public ICollection<ProductGallery>? productGallery { get; set; }

    }
}
