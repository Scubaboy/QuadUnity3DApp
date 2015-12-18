using Assets.MonoBehaviour_Extensions.SignalR.Controllers;
using Assets.MonoBehaviour_Extensions.SignalR.Interfaces.SignalRClientContainers;
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
using Assets.MonoBehaviour_Extensions.SignalR.Container_Events.ActiveQuadContainer;
using Assets.Services.SignalR.MsgParser.JsonParser;

namespace Assets.MonoBehaviour_Extensions.SignalR.Clients
{
    public class ActiveQuadSignalRClient : SignalRClient, ISignalRActiveQuadContainer
    {
        /// <summary>
        /// 
        /// </summary>
        private ISignalRClientController clientController;

        /// <summary>
        /// 
        /// </summary>
        public event ActiveQuadsUpdateEventHandler ActiveQuads;

        /// <summary>
        /// Setup the clients configuration.
        /// </summary>
        void Awake()
        {
            this.HubMethodTypeMapping = new Dictionary<Type, string>
            {
                {typeof(ActiveQuad), "UpdateQuad" }
            };

            this.ClientMethodTypeMapping = new Dictionary<string, ISignalRMsgParserJson>
            {
                { "ActiveQuads", new HubToClientMsgParserJson<List<ActiveQuad>>()}
            };

         //   this.ParamsToClass = new MsgParamsToClass();
            this.HubConnectionParams = new HubConnectionParams("ActiveQuadsHub", "localhost:8080", false);

            //Get the SignalR Client Controller.
            this.clientController = GameObject
                .FindObjectOfType<SignalRClientController>()
                .GetComponent<SignalRClientController>() as ISignalRClientController;
            
            //Regsiter SignalRlient Container callback
            this.Register<List<ActiveQuad>>("ActiveQuads", this.ActiveQuadsCallback);
        }

        /// <summary>
        /// Maintains a list of all connected quads.
        /// </summary>
        /// <param name="activeQuads"></param>
        private void ActiveQuadsCallback(List<ActiveQuad> activeQuads)
        {
            this.OnActiveQuadUpdate(new ActiveQuadsUpdateEventArgs(activeQuads));
        }

        void Start()
        {
            //Register the client with the client controller.
            this.clientController.RegisterClient(this);

            //Test message send
            this.Send<ActiveQuad>(new ActiveQuad() { QuadId = "ff" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        protected virtual void OnActiveQuadUpdate(ActiveQuadsUpdateEventArgs args)
        {
            ActiveQuadsUpdateEventHandler activeQuadHandler = this.ActiveQuads;

            if (activeQuadHandler != null)
            {
                activeQuadHandler(this, args);
            }
        }

    }
}
