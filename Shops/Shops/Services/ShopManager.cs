namespace Shops
{
    public class ShopManager
    {
        private int uniqueid = 0;
        private List<Shop> shops;

        public ShopManager()
        {
            shops = new List<Shop>();
        }

        public void AddShop(Shop shop)
        {
            if (shop == null) throw new NullException("shop is null");
            shop.Id = uniqueid + 1;
            shops.Add(shop);
        }

        public List<Shop> GetShops() => shops.ToList();

        public Shop FindCheapest(Buyer buyer)
        {
            if (buyer == null) throw new NullException("buyer is null");
            return shops.MinBy(p =>
            {
                if (p != null) return p.GetTotalPrice(buyer);
                return 0;
            });
        }
    }
}