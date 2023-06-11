namespace Banks.Accounts
{
    public class DebitAccount : Account
    {
        private double accruedMoney;

        public DebitAccount(int id, double money, double fixedPercentage, DateTime openDate)
        {
            this.Id = id;
            this.Balance = money;
            this.FixedPercentage = fixedPercentage;

            this.OpenDate = openDate;
            this.AccountType = AccountType.Debit;
            this.LastBankOperation = openDate;
            this.accruedMoney = 0;
        }

        public double FixedPercentage { get; set; }

        public override void CalcBankOperation(double amount, DateTime dateTime)
        {
            if (dateTime.Month == LastBankOperation.Month && dateTime.Year == LastBankOperation.Year)
            {
                double days = (dateTime - LastBankOperation).TotalDays;
                double income = GetDaysIncome(days);
                LastBankOperation = dateTime;
                accruedMoney += income;
            }
            else
            {
                DateTime newDayMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
                Accrue(newDayMonth);

                double days = (dateTime - LastBankOperation).TotalDays;
                double income = GetDaysIncome(days);
                LastBankOperation = dateTime;
                accruedMoney += income;
            }
        }

        public override bool IsValidBalance(double money, DateTime dateTime)
        {
            return (this.Balance - money) > 0;
        }

        public override void CalcBankShowOperation(DateTime dateTime)
        {
            double days = (dateTime - LastBankOperation).TotalDays;
            double income = GetDaysIncome(days);
            Console.WriteLine(string.Format("Дебетовый счет. Количество дней {0}. Начисления {1}", (int)days, income));
        }

        private void Accrue(DateTime newDayMonth)
        {
            double days = (newDayMonth - LastBankOperation).TotalDays;
            accruedMoney += GetDaysIncome(days);
            this.Balance += accruedMoney;
            LastBankOperation = newDayMonth;
            accruedMoney = 0;
        }

        private double GetDaysIncome(double days)
        {
            return Math.Round((((FixedPercentage / 365) * Balance) / 100) * days, 2);
        }
    }
}