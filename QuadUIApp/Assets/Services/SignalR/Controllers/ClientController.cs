namespace Assets.Services.SignalR.Controllers
{
    using Interfaces;
    using System.Collections.Generic;
    using UnityEngine;

    public class ClientController : MonoBehaviour, ISignalRClientController
    {
        protected ISignalRHubConnCtrl hubConnCtrl;
        protected List<ISignalRClient> registeredClients;
        protected ISignalRTransportFactory transportFactory;
        protected ProcessCycle processCycle;

        /// <summary>
        /// Processing cycle states.
        /// </summary>
        protected enum ProcessCycle
        {
            /// <summary>
            /// Read messages from the clients transport protocol.
            /// </summary>
            Read,

            /// <summary>
            /// Send messages to the clients transport protocol.
            /// </summary>
            Send
        }

        public bool RegisterClient(ISignalRClient client)
        {
            var result = true;

            if (!this.registeredClients.Contains(client))
            {
                var trans = this.transportFactory.CreateWebSocketTransport(client.HubName, client.HostServerUrl, client.UseSecureConnection);

                result = hubConnCtrl.ConnectToHub(client, trans);
                this.registeredClients.Add(client);
            }

            return result;
        }

        public bool UnRegisterClient(ISignalRClient client)
        {
            var result = true;

            if (this.registeredClients.Contains(client))
            {
                this.hubConnCtrl.RemoveClientFromHub(client);
                this.registeredClients.Remove(client);
            }

            return result;
        }

        
    }
}
