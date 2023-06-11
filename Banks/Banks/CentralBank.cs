using Banks.Accounts;
using Banks.Builder;
using Banks.Observer;

namespace Banks
{
    public class CentralBank : ICentralBankObserver
    {
        private static CentralBank centralBank;
        private readonly List<IBankObserver> bankObservers;
        private readonly Dictionary<string, Bank> banks;

        private CentralBank()
        {
            bankObservers = new List<IBankObserver>();
            banks = new Dictionary<string, Bank>();
        }

        public static CentralBank GetCentralBank()
        {
            if (centralBank == null)
            {
                centralBank = new CentralBank();
            }

            return centralBank;
        }

        public Bank AddNewBank(string name, int fixedPercentage, int creditLimit, int fees, IDepositCalc depositCalc)
        {
            Bank bank = new Bank(name, fixedPercentage, depositCalc, creditLimit, fees, this, new ClientBuilder());
            banks.Add(name, bank);
            bankObservers.Add(bank);

            return bank;
        }

        public void AddNotifyBank(IBankObserver bank)
        {
            bankObservers.Add(bank);
        }

        public void NotifyObservers(string massage)
        {
            foreach (IBankObserver observer in bankObservers)
            {
                observer.Notification("Нужно начислять остаток или комиссию");
            }
        }

        public void RemoveNotifyBank(IBankObserver bank)
        {
            bankObservers.Remove(bank);
        }

        public Bank FindBank(string name)
        {
            Bank bank = banks[name];
            return bank;
        }

        public List<string> GetBanks()
        {
            return banks.Select(s => s.Key).ToList();
        }
    }
}