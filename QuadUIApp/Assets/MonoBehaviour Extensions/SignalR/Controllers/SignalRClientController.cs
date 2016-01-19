namespace Assets.MonoBehaviour_Extensions.SignalR.Controllers
{
    using UnityEngine;
    using System.Collections.Generic;
    using Factories.Transport.Factory;
    using Services.SignalR.Controllers;
    using Services.SignalR.Interfaces;

    public class SignalRClientController : ClientController
    {
        /// <summary>
        /// Configre the Client Controller.
        /// </summary>
        void Awake()
        {
            this.registeredClients = new List<ISignalRClient>();
            this.processCycle = ProcessCycle.Send;
            transportFactory = GameObject.FindObjectOfType<SignalRTransportFactory>() as ISignalRTransportFactory;
            this.hubConnCtrl = GameObject.FindObjectOfType<SignalRHubConnController>() as ISignalRHubConnCtrl;
        }

        void Update()
        {
            //Cycle through the connected clients and post/send messages.
            switch (this.processCycle)
            {
                case ProcessCycle.Read:
                    {
                        this.registeredClients.ForEach(client =>
                        {
                            client.MsgRcved(this.hubConnCtrl.GetMesssges(client));
                        });

                        this.processCycle = ProcessCycle.Send;
                        break;
                    }
                case ProcessCycle.Send:
                    {
                        this.registeredClients.ForEach(client =>
                        {
                            this.hubConnCtrl.PostMessage(client.GetNextMsgToSend(), client);
                        });

                        this.processCycle = ProcessCycle.Read;
                        break;
                    }
            }
        }
    }
}
