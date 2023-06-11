namespace Shops
{
    public class Delivery
    {
        public Delivery(List<Product> productlist)
        {
            Productlist = productlist;
        }

        private List<Product> Productlist { get; set; }

        public void AddProductsForDelivery(List<Product> delivery) => Productlist = delivery ?? throw new NullException("Лист пуст");

        public List<Product> GetProducts()
        {
            return Productlist;
        }
    }
}
