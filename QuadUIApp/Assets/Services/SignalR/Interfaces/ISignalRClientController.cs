using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Interfaces
{
    public interface ISignalRClientController
    {
        bool RegisterClient(ISignalRClient client);

        bool UnRegisterClient(ISignalRClient client);
    }
}
