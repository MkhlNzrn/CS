namespace Banks.Accounts
{
    public class DepositCalc : IDepositCalc
    {
        public double Calculate(double money)
        {
            if (money < 10000)
                return 3;
            else if (money >= 10000 && money <= 20000)
                return 3.5;
            else
                return 4;
        }
    }
}
