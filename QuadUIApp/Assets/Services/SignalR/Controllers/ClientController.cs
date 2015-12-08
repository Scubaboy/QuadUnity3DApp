using Assets.Services.Interfaces;
using Assets.Services.SignalR.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Controllers
{
    public class ClientController : ISignalRClientController
    {
        private ISignalRHubConnCtrl hubConnCtrl;

        private Dictionary<ISignalRClient, ISignalRTransportCtrl> registeredClients;

        public ClientController(ISignalRHubConnCtrl hubConnCtrl)
        {
            this.hubConnCtrl = hubConnCtrl;
            this.registeredClients = new Dictionary<ISignalRClient, ISignalRTransportCtrl>();
        }

        public bool RegisterClient(ISignalRClient client)
        {
            var result = true;

            if (!this.registeredClients.ContainsKey(client))
            {
                var trans = new SignalRTransportWS(client,"https://www.mysignalr");

                result = hubConnCtrl.ConnectToHub(client, trans);
                this.registeredClients.Add(client,trans);
            }

            return result;
        }

        public bool UnRegisterClient(ISignalRClient client)
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}
