using Maison_Sucree.Web.Utility;
using System.ComponentModel.DataAnnotations;

namespace Maison_Sucree.Web.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageLocalPath { get; set; }
        [Range(1,100)]
        public int Count { get; set; } = 1;
        [AllowedExtensions(new string[] {".jpg",".png"})]
        public IFormFile? Image { get; set; }
    }
}
