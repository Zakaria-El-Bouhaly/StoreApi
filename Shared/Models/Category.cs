namespace Shared.Models
{
    public class Category
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
