namespace Banks.Accounts
{
    public class NewDepositCalc : IDepositCalc
    {
        public double Calculate(double money)
        {
            if (money < 20000)
                return 2.1;
            else if (money >= 20000 && money <= 50000)
                return 2.5;
            else
                return 3;
        }
    }
}
