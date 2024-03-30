using System.ComponentModel.DataAnnotations;

namespace Shared.Dto
{
    public class CategoryDto
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }


    }
}
