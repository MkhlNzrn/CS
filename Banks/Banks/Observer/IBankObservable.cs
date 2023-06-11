using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Banks.Accounts;

namespace Banks.Observer
{
    public interface IBankObservable
    {
        void RemoveNotifyClient(IClientObserver client);
        void NotifyObservers(string massage);
        void NotifyObservers(AccountType accountType, string message);
    }
}
