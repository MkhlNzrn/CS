namespace Shops
{
    public class Shop
    {
        public Shop(string shopName, string shopAdress)
        {
            Name = shopName ?? throw new NullException("Name is null");
            Adress = shopAdress ?? throw new NullException("adress is null");
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public List<Product> ListProducts { get; set; } = new List<Product>();

        public List<Product> GetProducts()
        {
            return ListProducts;
        }

        public void AddProducts(Delivery delivery)
        {
            if (delivery.GetProducts() == null) throw new NullException("delivery is null");
            if (ListProducts != null) ListProducts.AddRange(delivery.GetProducts());
        }

        public void AddProduct(Product product)
        {
            if (product == null) throw new NullException("No product");
            ListProducts?.Add(product);
        }

        public void ChangePrice(string name1, double newPrice)
        {
            if (ListProducts != null)
            {
                Product product = ListProducts.FirstOrDefault(s => s.Name == name1);
                if (product == null)
                {
                    throw new NoProductException("no product");
                }

                product.Price = newPrice;
            }
        }

        public void Buy(Buyer buyer)
        {
            double totalPrice = GetTotalPrice(buyer);

            if (totalPrice > buyer.Money)
                throw new NotEnoughtMoneyException("На балансе недостаточно средств");

            RecalculateProducts(buyer);
            buyer.Money -= totalPrice;
        }

        public void RecalculateProducts(Buyer buyer)
        {
            if (buyer == null) throw new NullException("buyer is null");
            if (ListProducts != null)
            {
                ListProducts = ListProducts.Select(s =>
                {
                    var shopping = buyer.Products.Find(p => p.Name == s.Name);
                    if (shopping != null)
                        s.Count -= shopping.Count;

                    return s;
                }).ToList();
            }
        }

        public bool IsAvailable(Product productBuyer, Product productShop)
        {
            if (productBuyer.Count > productShop.Count)
                throw new NotEnoughtProductsException("Недостаточно товара");

            return true;
        }

        public double GetTotalPrice(Buyer buyer)
        {
            if (buyer == null) throw new NullException("buyer is null");
            if (ListProducts != null)
            {
                var totalPrice = buyer.Products
                    .Join(
                        ListProducts,
                        buyerProducts => buyerProducts.Name,
                        shop => shop.Name,
                        (buyerProducts, shop) => new { buyerProducts, shop })
                    .Where(s => IsAvailable(s.buyerProducts, s.shop))
                    .Sum(s => s.buyerProducts.Count * s.shop.Price);
                return totalPrice;
            }

            return 0;
        }
    }
}
