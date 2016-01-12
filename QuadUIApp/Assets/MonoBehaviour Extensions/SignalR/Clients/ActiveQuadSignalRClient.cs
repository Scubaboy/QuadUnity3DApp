namespace Assets.MonoBehaviour_Extensions.SignalR.Clients
{
    using Container_Events.ActiveQuadContainer;
    using Controllers;
    using Interfaces.SignalRClientContainers;
    using Services.Interfaces;
    using Services.SignalR.Client;
    using Services.SignalR.Models;
    using Services.SignalR.MsgParser.JsonParser;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
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
        public event QuadSelectionConfirmedEventHandler QuadSelectionConfirmed;

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
                { "ActiveQuads", new HubToClientMsgParserJson<List<ActiveQuad>>()},
                { "ConfirmQuadSelection", new HubToClientMsgParserJson<QuadSelectionConfirmed>() }
            };

            this.HubConnectionParams = new HubConnectionParams("ActiveQuadsHub", "localhost:8080", false);

            //Get the SignalR Client Controller.
            this.clientController = GameObject
                .FindObjectOfType<SignalRClientController>()
                .GetComponent<SignalRClientController>() as ISignalRClientController;
            
            //Regsiter SignalRlient Container callback
            this.Register<List<ActiveQuad>>("ActiveQuads", this.ActiveQuadsCallback);
            this.Register<QuadSelectionConfirmed>("ConfirmQuadSelection", this.QuadSelectionConfirmedCallback);
        }

        /// <summary>
        /// Maintains a list of all connected quads.
        /// </summary>
        /// <param name="activeQuads"></param>
        private void ActiveQuadsCallback(List<ActiveQuad> activeQuads)
        {
            this.OnActiveQuadUpdate(new ActiveQuadsUpdateEventArgs(activeQuads));
        }

        private void QuadSelectionConfirmedCallback(QuadSelectionConfirmed confirmation)
        {

        }

        void Start()
        {
            //Register the client with the client controller.
            this.clientController.RegisterClient(this);
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

        protected virtual void OnConfirmQuadSelection(QuadSelectionConfirmedEventArgs args)
        {
            QuadSelectionConfirmedEventHandler confirmSelectionHandler = this.QuadSelectionConfirmed;

            if (confirmSelectionHandler != null)
            {
                confirmSelectionHandler(this, args);
            }

        }

        public void ConfirmQuadSelection(ActiveQuad selectedQuad)
        {
            this.Send<ActiveQuad>(selectedQuad);
        }
    }
}
