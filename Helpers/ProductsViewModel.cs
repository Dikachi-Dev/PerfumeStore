using PerfumeStore.Models;

namespace PerfumeStore.Helpers
{
    public class ProductsViewModel
    {
        public List<Product>? Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
