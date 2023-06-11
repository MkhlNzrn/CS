using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banks.Observer
{
    public interface ICentralBankObserver // observable rename
    {
        void AddNotifyBank(IBankObserver bank);
        void RemoveNotifyBank(IBankObserver bank);
        void NotifyObservers(string massage);
    }
}
