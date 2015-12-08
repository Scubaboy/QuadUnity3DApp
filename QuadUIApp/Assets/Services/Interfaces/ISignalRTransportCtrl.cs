using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRTransportCtrl
    {
        bool Connect();

        bool Send(string msg);

        List<ReceivedSignalRMsg> Read();

        void Close();
    }
}
