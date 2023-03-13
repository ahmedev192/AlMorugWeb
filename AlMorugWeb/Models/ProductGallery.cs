using static System.Reflection.Metadata.BlobBuilder;

namespace AlMorugWeb.Models
{
    public class ProductGallery
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        public Product Product { get; set; }
    }
}
