namespace AspNetCoreMVC.Models
{
    public class Product
    {
        public int PCode { get; set; }
        public string Name { get; set; }
        public int QtyInStock { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
    }
}
