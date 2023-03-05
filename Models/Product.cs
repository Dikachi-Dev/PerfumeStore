using System.Drawing.Drawing2D;

namespace PerfumeStore.Models
{
    public class Product
    {
        public Product()
        {
            this.Categorys = new List<Category>();
            this.Stocks = new List<Stock>();
        }
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public int Size { get; set; }
        public List<Category>? Categorys { get; set; }
        public List<Stock>? Stocks { get; set; }

    }
}

