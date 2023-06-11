namespace Shops;

public class Product
{
   public Product(string productName, double productPrice, int productCount)
       {
           Name = productName ?? throw new NullException("name is null");
           if (productPrice < 0) throw new InvalidBalanceException("минус на ценнике");
           Price = productPrice;
           if (productCount < 0) throw new NoProductException("invalid count");
           Count = productCount;
       }

   public string Name { get; set; }
   public double Price { get; set; }
   public int Count { get; set; }
}