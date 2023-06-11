using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banks.Accounts;

namespace Banks.Commands
{
    public class Deposit : ITransaction
    {
        private Account account;
        private double amount;
        private int id;

        public Deposit(int id, Account account, double amount, DateTime dateTime)
        {
            this.id = id;
            this.account = account;
            this.amount = amount;
            this.TransactionDate = dateTime;
        }

        public double Amount => amount;
        public double Balance => this.account.Balance;
        public DateTime TransactionDate { get; }

        public void Execute()
        {
            this.account.CalcBankOperation(amount, TransactionDate);
            this.account.Balance = this.account.Balance + amount;
        }

        public void Undo()
        {
            this.account.Balance = this.account.Balance - amount;
        }
    }
}