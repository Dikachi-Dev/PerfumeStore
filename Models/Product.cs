namespace PerfumeStore.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Tag { get; set; }
        public int Size { get; set; }
        public Category? CategoryId { get; set; }
        public Stock Stock { get; set; }

    }
}
