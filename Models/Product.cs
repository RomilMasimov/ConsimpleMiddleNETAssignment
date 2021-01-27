namespace ConsimpleMiddleNetAssignment.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string VendorCode { get; set; }
        public decimal Price { get; set; }
    }
}