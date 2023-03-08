using System.Drawing.Drawing2D;

namespace PerfumeStore.Models
{
    public class Product
    {
        public Product()
        {
           
            this.Stocks = new List<Stock>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public int Size { get; set; }
        public string Category { get; set; }
        public decimal? Price { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
        public List<Stock>? Stocks { get; set; }

    }
}

