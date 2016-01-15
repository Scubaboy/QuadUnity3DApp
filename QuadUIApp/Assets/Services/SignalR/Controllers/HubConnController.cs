using Assets.Services.Interfaces;
using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Services.SignalR.Controllers
{
    class HubConnController : MonoBehaviour, ISignalRHubConnCtrl
    {
        protected Dictionary<ISignalRClient, ISignalRTransportCtrl> clientTransportMap;

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

        public List<ReceivedSignalRMsg> GetMesssges(ISignalRClient client)
        {
            List<ReceivedSignalRMsg> msg = null;

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
                this.clientTransportMap[client].PostToSendQueue(msg);
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
