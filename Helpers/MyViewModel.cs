using PerfumeStore.Models;

namespace PerfumeStore.Helpers
{
    public class MyViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
