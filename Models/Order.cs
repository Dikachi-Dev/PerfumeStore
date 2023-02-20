namespace PerfumeStore.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string OrderRef { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }


    }
}
