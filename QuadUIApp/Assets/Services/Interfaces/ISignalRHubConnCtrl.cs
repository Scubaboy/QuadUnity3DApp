using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRHubConnCtrl
    {
        bool ConnectToHub(ISignalRClient client, ISignalRTransportCtrl transProtocol);

        List<ReceivedSignalRMsg> GetMesssges(ISignalRClient client);

        void PostMessage(string msg, ISignalRClient client);

        bool RemoveClientFromHub(ISignalRClient client);
        
        /// <summary>
        /// 
        /// </summary>
        void Update();
    }
}
