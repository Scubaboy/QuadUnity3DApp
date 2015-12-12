using Assets.MonoBehaviour_Extensions.SignalR.Controllers;
using Assets.Services.Interfaces;
using Assets.Services.SignalR.Client;
using Assets.Services.SignalR.Models;
using Assets.Services.SignalR.MsgParser;
using Assets.Services.SignalR.MsgToClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.MonoBehaviour_Extensions.SignalR.Clients
{
    public class ActiveQuadSignalRClient : SignalRClient
    {
        private ISignalRClientController clientController;

        /// <summary>
        /// Setup the clients configuration.
        /// </summary>
        void Awake()
        {
            this.HubMethodTypeMapping = new Dictionary<Type, string>
            {
                {typeof(ActiveQuad), "UpdateQuad" }
            };

            this.ClientMethodTypeMapping = new Dictionary<string, Type>
            {
                {"ActiveQuad", typeof(ActiveQuad)}
            };

            this.HubToClientMsgParser = new HubToClientMsgParser();
            this.ParamsToClass = new MsgParamsToClass();
            this.HubConnectionParams = new HubConnectionParams("ActiveQuadsHub", "localhost:8080", false);

            //Get the SignalR Client Controller.
            this.clientController = GameObject
                .FindObjectOfType<SignalRClientController>()
                .GetComponent<SignalRClientController>() as ISignalRClientController;
        }

        void Start()
        {
            //Register the client with the client controller.
            this.clientController.RegisterClient(this);

            //Test message send
            this.Send<ActiveQuad>(new ActiveQuad() { QuadId = "ff" });
        }

    }
}
