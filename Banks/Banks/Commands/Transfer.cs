using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banks.Accounts;

namespace Banks.Commands
{
    /// <summary>
    /// Перевод.
    /// </summary>
    public class Transfer : ITransaction
    {
        private Account account;
        private double amount;
        private int id;
        private CentralBank centralBank;
        private Client toClient;
        private string toNameBank;

        public Transfer(int id, Account account, double amount, DateTime dateTime, CentralBank centralBank, Client toClient, string toNameBank)
        {
            this.id = id;
            this.account = account;
            this.amount = amount;
            this.TransactionDate = dateTime;
            this.centralBank = centralBank;
            this.toClient = toClient;
            this.toNameBank = toNameBank;
        }

        public double Amount => amount;
        public double Balance => this.account.Balance;
        public DateTime TransactionDate { get; }

        public void Execute()
        {
            if (account.IsValidBalance(amount, TransactionDate))
            {
                this.account.CalcBankOperation(amount, TransactionDate);
                this.account.Balance = this.account.Balance - amount;
                Bank toBank = centralBank.FindBank(toNameBank);
                toBank.Deposit(toClient, amount, TransactionDate);
            }
        }

        public void Undo()
        {
            this.account.Balance = this.account.Balance + amount;
            Bank toBank = centralBank.FindBank(toNameBank);
            toBank.Withdraw(toClient, amount, TransactionDate);
        }
    }
}