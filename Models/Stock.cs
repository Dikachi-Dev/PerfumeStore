namespace PerfumeStore.Models
{
    public class Stock
    {
        public Product ProductId { get; set; }
        public Category CategoryId { get; set; }
        public int Quantity { get; set; }
    }
}
