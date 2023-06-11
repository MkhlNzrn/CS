namespace Banks.Accounts
{
    public class DepositAccount : Account
    {
        private int periodDepositDays = 10;
        private double percentage;
        private double accruedMoney;

        public DepositAccount(int id, double money, DateTime openDate, double percentage)
        {
            this.Id = id;
            this.Balance = money;
            this.OpenDate = openDate;
            this.percentage = percentage;
            this.LastBankOperation = openDate;
            this.AccountType = AccountType.Deposit;
            this.accruedMoney = 0;
        }

        public override void CalcBankOperation(double amount, DateTime dateTime)
        {
            if (dateTime.Month == LastBankOperation.Month && dateTime.Year == LastBankOperation.Year)
            {
                double days = (dateTime - LastBankOperation).TotalDays;
                double income = (percentage * Balance / 100) * days;
                LastBankOperation = dateTime;
                accruedMoney += income;
            }
            else
            {
                DateTime newDayMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
                Accrue(newDayMonth);

                double income = GetIncome();
                LastBankOperation = dateTime;
                accruedMoney += income;
            }
        }

        public override bool IsValidBalance(double money, DateTime dateTime)
        {
            return (dateTime - OpenDate).TotalDays > periodDepositDays;
        }

        public override void CalcBankShowOperation(DateTime dateTime)
        {
            double days = (dateTime - LastBankOperation).TotalDays;
            double income = GetIncome();
            Console.WriteLine(string.Format("Депозитный счет. Количество дней {0}. Начисления {1}", (int)days, income));
        }

        private double GetIncome()
        {
            return percentage * Balance / 100;
        }

        private void Accrue(DateTime newDayMonth)
        {
            accruedMoney += GetIncome();
            this.Balance += accruedMoney;
            LastBankOperation = newDayMonth;
            accruedMoney = 0;
        }
    }
}