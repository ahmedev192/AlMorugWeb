using static System.Reflection.Metadata.BlobBuilder;

namespace AlMorugWeb.Models
{
    public class ProductGallery
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string URL { get; set; } = string.Empty;

        public Product Product { get; set; } = new Product();
    }
}
