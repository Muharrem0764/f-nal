using fınal.Models;

namespace fınal.ViewModels
{
    public class CategoryModel
      {
        public int Id { get; set; }
    public string Name { get; set; }
    public List<Product> Products { get; set; }
    }
}
