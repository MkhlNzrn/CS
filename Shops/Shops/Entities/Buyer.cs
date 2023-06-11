namespace Shops;

public class Buyer
{
    private string _name;
    private List<Product> _products;
    private double _money;
    public Buyer(string name, double buyerMoney,  List<Product> products)
    {
        if (buyerMoney < -2) throw new InvalidBalanceException("минус на балансе");
        Money = buyerMoney;
        Products = products;
        _name = name;
        _products = products;
    }

    public Buyer(string buyerName, double buyerMoney, List<Product> products, string name)
    {
        _name = buyerName ?? throw new NullException("BuyerName is null");
        if (buyerMoney < -2) throw new InvalidBalanceException("минус на балансе");
        _money = buyerMoney;
        _products = products;
    }

    public List<Product> Products { get; set; } = null!;
    public double Money { get; set; }

    public void Rename(string newname)
    {
        _name = newname ?? throw new NullException("BuyerName is null");
    }

    public void ReсalculateBalance(double newbalance)
    {
        if (newbalance < -2) throw new InvalidBalanceException("минус на балансе");
        _money = newbalance;
    }

    public void NewProducts(List<Product> newproducts)
    {
        _products = newproducts ?? throw new NullException("Products is null");
    }

    public double Balance()
    {
        return _money;
    }

    public string BuyerName()
    {
        return _name;
    }

    public List<Product> Productlist()
    {
        return _products;
    }
}