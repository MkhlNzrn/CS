using System.Diagnostics;
using Shops;
using Xunit;

namespace Shops.Test;

public class Shop
{
    [Fact]
    public void Test1()
    {
        var products1 = new List<Product>();
        var buyer = new Buyer("name", 1000, products1);
    }

    [Fact]
    public void Test2()
    {
        var shopManager = new ShopManager();
        var shop1 = new Shops.Shop("Name1", "adress1");
        shopManager.AddShop(shop1);
        var product1 = new Product("name1", 10, 11);
        var product2 = new Product("name2", 8, 11);
    }

    [Fact]
    public void Test3()
    {
        var products1 = new List<Product>();
        var buyer = new Buyer("name", 1000, products1);
        var shop1 = new Shops.Shop("Name1", "adress1");
        var product1 = new Product("name1", 10, 11);
        shop1.AddProducts(new Delivery(products1));
        shop1.Buy(buyer);
    }

    [Fact]
    public void Test4()
    {
        var products1 = new List<Product>();
        var product1 = new Product("name1", 10, 11);
        products1.Add(product1);
        var buyer = new Buyer("name", 1000, products1);
        var shop1 = new Shops.Shop("Name1", "adress1");
        shop1.AddProducts(new Delivery(products1));
        var count1 = buyer.Products[0].Count;
        Debug.Assert(shop1.ListProducts != null, "shop1.ListProducts != null");
        var count0 = shop1.ListProducts[0].Count;
        shop1.Buy(buyer);
        Assert.Equal(count0 - count1, shop1.ListProducts[0].Count);
    }
}