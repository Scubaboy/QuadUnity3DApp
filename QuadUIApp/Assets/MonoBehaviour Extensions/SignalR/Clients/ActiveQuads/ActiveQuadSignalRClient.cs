namespace Assets.MonoBehaviour_Extensions.SignalR.Clients.ActiveQuads
{
    using Services.Models;
    using Controllers;
    using Services.SignalR.Client;
    using Services.SignalR.MsgParser.JsonParser;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using Services.SignalR.Interfaces;
    using Services.ViewSystem.View.Interfaces;
    using Container_EventsCallbacks.ActiveQuadContainer;
    using Exceptions;
    using Interfaces;
    using ViewSystem.Views.Fixed.Status_Feeds.Interfaces;
    using ViewSystem.Views.Fixed.Status_Feeds;

    /// <summary>
    /// 
    /// </summary>
    public class ActiveQuadSignalRClient : SignalRClient, ISignalRActiveQuadViewAccess
    {
        private class ViewMapper
        {

            public QuadSelectionConfirmedHandler ConfirmHandler { get; set; }

            public ActiveQuadsUpdateHandler ActiveQuadsHandler { get;  set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private ISignalRClientController clientController;

        private Dictionary<IView, ViewMapper> ViewHandlerMappers;

        private Dictionary<string,IView> waitingSelectionConfirmation;

        private IStatusFeedNotify statusFeedsNotify;

        /// <summary>
        /// Setup the clients configuration.
        /// </summary>
        void Awake()
        {
            this.ViewHandlerMappers = new Dictionary<IView, ViewMapper>();
            this.waitingSelectionConfirmation = new Dictionary<string, IView>();

            this.HubMethodTypeMapping = new Dictionary<Type, string>
            {
                {typeof(ActiveQuad), "UpdateQuad" }
            };

            this.ClientMethodTypeMapping = new Dictionary<string, ISignalRMsgParserJson>
            {
                { "ActiveQuads", new HubToClientMsgParserJson<List<ActiveQuad>>()},
                { "ConfirmQuadSelection", new HubToClientMsgParserJson<Services.Models.QuadSelectionConfirmed>() }
            };

            this.HubConnectionParams = new HubConnectionParams("ActiveQuadsHub", "localhost:8080", false);

            //Get the SignalR Client Controller.
            this.clientController = GameObject
                .FindObjectOfType<SignalRClientController>()
                .GetComponent<SignalRClientController>() as ISignalRClientController;

            //Get StatusFeeds view
            this.statusFeedsNotify = FindObjectOfType<StatusFeedsView>()
                .GetComponent<StatusFeedsView>() as IStatusFeedNotify;

            //Regsiter SignalRlient Container callback
            this.Register<List<ActiveQuad>>("ActiveQuads", this.ActiveQuadsCallback);
            this.Register<Services.Models.QuadSelectionConfirmed>("ConfirmQuadSelection", this.QuadSelectionConfirmedCallback);
        }

        /// <summary>
        /// Maintains a list of all connected quads.
        /// </summary>
        /// <param name="activeQuads"></param>
        private void ActiveQuadsCallback(List<ActiveQuad> activeQuads)
        {
           foreach (var viewMapper in this.ViewHandlerMappers.Values)
            {
                viewMapper.ActiveQuadsHandler(new ActiveQuadsUpdate(activeQuads));
            }
        }

        private void QuadSelectionConfirmedCallback(Services.Models.QuadSelectionConfirmed confirmation)
        {
            if (this.waitingSelectionConfirmation.ContainsKey(confirmation.TheQuad.QuadId))
            {
                if (ViewHandlerMappers.ContainsKey(this.waitingSelectionConfirmation[confirmation.TheQuad.QuadId]))
                {
                    ViewHandlerMappers[this.waitingSelectionConfirmation[confirmation.TheQuad.QuadId]].ConfirmHandler(new Container_EventsCallbacks.ActiveQuadContainer.QuadSelectionConfirmed(confirmation.TheQuad, confirmation.Confirmed));
                    this.waitingSelectionConfirmation.Remove(confirmation.TheQuad.QuadId);
                }
                else
                {
                    //log to status view.
                    this.statusFeedsNotify.PostNotification("Received quad confirmation nofirmation but no recipient registered", NotificationType.Error);
                }
            }
            else
            {
                // log to status view.
            }
        }

        void Start()
        {
            //Register the client with the client controller.
            this.clientController.RegisterClient(this);
        }

        public RequestQuadConfirmation ConfirmQuadSelection(IView registerdView, ActiveQuad selectedQuad)
        {
            RequestQuadConfirmation requestResult = RequestQuadConfirmation.RequestSent;

            if (this.ViewHandlerMappers.ContainsKey(registerdView))
            {
                this.Send<ActiveQuad>(selectedQuad);

                if (!this.waitingSelectionConfirmation.ContainsKey(selectedQuad.QuadId))
                {
                    this.waitingSelectionConfirmation.Add(selectedQuad.QuadId, registerdView);
                }
                else
                {
                    requestResult = RequestQuadConfirmation.QuadAlreadyPending;
                }
            }
            else
            {
                throw new ViewNotRegisteredException("Unable to send Quad confirmation request view " + registerdView.ToString() +" not registered.");
            }

            return requestResult;
        }

        public void RegisterView(IView view)
        {
            if (!this.ViewHandlerMappers.ContainsKey(view))
            {
                this.ViewHandlerMappers.Add(view, new ViewMapper());
            }
            else
            {
                throw new ViewAlreadyRegisteredException("View already registered: " + view.ToString());
            }
        }

        public void RegisterActiveQuadUpdateHandler(IView registerView, ActiveQuadsUpdateHandler updateHandler)
        {
            if (this.ViewHandlerMappers.ContainsKey(registerView))
            {
                this.ViewHandlerMappers[registerView].ActiveQuadsHandler = updateHandler;
            }
            else
            {
                throw new ViewNotRegisteredException("Unable to assign updateHandler View : " + registerView.ToString() + " not registered with " + this.ToString());
            }
        }

        public void RegisterConfirmedhandler(IView registerView, QuadSelectionConfirmedHandler selectionConfirmedHandler)
        {
            if (this.ViewHandlerMappers.ContainsKey(registerView))
            {
                this.ViewHandlerMappers[registerView].ConfirmHandler = selectionConfirmedHandler;
            }
            else
            {
                throw new ViewNotRegisteredException("Unable to assign selectionConfirmedhandler View : " + registerView.ToString() + " not registered with " + this.ToString());
            }
        }
    }
}
