using Shared.Models;
using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]

        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(200)]
        [Required]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        
        public ICollection<int> CategoryIds { get; set; }

    }
}
