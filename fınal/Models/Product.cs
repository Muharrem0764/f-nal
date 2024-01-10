namespace fınal.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool Status { get; set; }

        public decimal Price { get; set; }


        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
