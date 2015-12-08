using Assets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Controllers
{
    class HubConnController : ISignalRHubConnCtrl
    {
        private Dictionary<ISignalRClient, ISignalRTransportCtrl> clientTransportMap;

        public HubConnController()
        {
            this.clientTransportMap = new Dictionary<ISignalRClient, ISignalRTransportCtrl>();

        }
        public bool ConnectToHub(ISignalRClient client, ISignalRTransportCtrl transProtocol)
        {
            var connResult = false;

            if (!this.clientTransportMap.ContainsKey(client))
            {
                connResult = transProtocol.Connect();

                if (connResult)
                {
                    this.clientTransportMap.Add(client, transProtocol);
                }
            }

            return connResult;
        }

        public List<string> GetMesssges(ISignalRClient client)
        {
            List<string> msg = null;

            if (this.clientTransportMap.ContainsKey(client))
            {
                msg = this.clientTransportMap[client].Read();
            }

            return msg;
        }

        public void PostMessage(string msg, ISignalRClient client)
        {
            if (this.clientTransportMap.ContainsKey(client))
            {
                this.clientTransportMap[client].Send(msg);
            }
        }

        public bool RemoveClientFromHub(ISignalRClient client)
        {
            var result = false;

            if (this.clientTransportMap.ContainsKey(client))
            {
                this.clientTransportMap[client].Close();
                this.clientTransportMap.Remove(client);

                result = true;
            }

            return result;
        }
    }
}
