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
        public ICollection<ProductGallery> productGallery { get; set; }

    }
}
