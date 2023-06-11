using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banks.Observer
{
    public interface IBankObserver
    {
        void Notification(string message);
    }
}
