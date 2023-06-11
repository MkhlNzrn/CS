namespace Banks.Accounts
{
    public class CreditAccount : Account
    {
        private int limit;
        private int fees;

        public CreditAccount(int id, double money, int limit, int fees, DateTime openDate)
        {
            this.Id = id;
            this.Balance = money;
            this.limit = limit;
            this.fees = fees;
            this.OpenDate = openDate;
            this.AccountType = AccountType.Credit;
        }

        public override void CalcBankOperation(double amount, DateTime dateTime)
        {
            LastBankOperation = dateTime;

            if (Balance < 0)
            {
                double commission = GetCommission();
                Balance -= commission;
            }
        }

        public override void CalcBankShowOperation(DateTime dateTime)
        {
            double days = (dateTime - LastBankOperation).TotalDays;
            double commission = 0;
            if (Balance < 0)
            {
                commission = GetCommission();
            }

            Console.WriteLine(string.Format("Кредитный счет. Количество дней {0}. Коммисия {1}", (int)days, commission));
        }

        public override bool IsValidBalance(double money, DateTime dateTime)
        {
            return money < limit;
        }

        private double GetCommission()
        {
            double commission = (fees * Balance) / 100;

            return commission;
        }
    }
}