namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Select
{
    using Services.ViewSystem.View.Controller;
    using UnityEngine.UI;
    using SignalR.Clients;
    using System.Linq;
    using System.Collections.Generic;
    using Services.Models;
    using SignalR.Container_EventsCallbacks.ActiveQuadContainer;
    using SignalR.Clients.ActiveQuads.Interfaces;
    using SignalR.Clients.ActiveQuads;

    public class SelectActiveQuadView : BaseViewController 
    {
        public Button ConfirmSelection;
        public Button CancelView;
        public Dropdown SelectQuad;
        public Button OkView;
        public Text Notifications;
        public Text SelectionBoxHeader;
        private ISignalRActiveQuadViewAccess activeQuadSignalRViewClient;
        private List<ActiveQuad> activeQuads;
        private ActiveQuad userSelectedQuad;
        private bool waitingSelectedQuadConfirmation;

        public override void Awake()
        {
            base.Awake();

            //Register with the ActiveQuad SignalR container.
            this.activeQuadSignalRViewClient = FindObjectOfType<ActiveQuadSignalRClient>()
                .GetComponent <ActiveQuadSignalRClient>() as ISignalRActiveQuadViewAccess;

            
        }

        void Start()
        {
            this.activeQuadSignalRViewClient.RegisterView(
                this);

            this.activeQuadSignalRViewClient.RegisterActiveQuadUpdateHandler(this, this.ActiveQuadSignalRNotifier_ActiveQuads);
            this.activeQuadSignalRViewClient.RegisterConfirmedhandler(this, this.ActiveQuadSignalRNotifier_QuadSelectionConfirmed);
        }

        protected override void ConfigureView()
        {
            //Setp local stores
            this.activeQuads = new List<ActiveQuad>();
            this.SelectQuad.options = new List<Dropdown.OptionData>();
            this.userSelectedQuad = null;
            this.waitingSelectedQuadConfirmation = false;
            this.SelectQuad.interactable = false;

            //Setup notification text
            this.Notifications.text = string.Empty;
            this.Notifications.gameObject.SetActive(false);
            this.SelectionBoxHeader.text = "Available Quads";

            //Setup the views buttons
            this.CancelView.onClick.RemoveAllListeners();
            this.CancelView.onClick.AddListener(this.Disable);
            this.CancelView.interactable = true;
            this.OkView.onClick.RemoveAllListeners();
            this.OkView.onClick.AddListener(this.Complete);
            this.OkView.interactable = false;

            this.ConfirmSelection.onClick.RemoveAllListeners();
            this.ConfirmSelection.onClick.AddListener(this.ConfirmQuadSelection);
            this.ConfirmSelection.interactable = false;

            //Setup dropdown selection
            this.SelectQuad.onValueChanged.RemoveAllListeners();
            this.SelectQuad.onValueChanged.AddListener(this.ProcessSelection);
           
        }

        private void ConfirmQuadSelection()
        {
            //Send selction request to Quad SignalR server to confirm still free
            var result = this.activeQuadSignalRViewClient.ConfirmQuadSelection(this,this.userSelectedQuad);

            switch (result)
            {
                case RequestQuadConfirmation.QuadAlreadyPending:
                    {
                        this.Notifications.text = "Quad Pending confirmation please select another";
                        this.Notifications.gameObject.SetActive(true);
                        this.ConfirmSelection.interactable = true;
                        this.waitingSelectedQuadConfirmation = false;
                        break;
                    }
                case RequestQuadConfirmation.RequestSent:
                    {
                        //Disable confirm selection button whilst processing the confirmation request.
                        this.ConfirmSelection.interactable = false;
                        this.waitingSelectedQuadConfirmation = true;
                        this.Notifications.text = string.Empty;
                        this.Notifications.gameObject.SetActive(false);
                        break;
                    }
            }
        }

        private void ActiveQuadSignalRNotifier_QuadSelectionConfirmed(SignalR.Container_EventsCallbacks.ActiveQuadContainer.QuadSelectionConfirmed args)
        {
            if (args.TheQuad.QuadId == this.userSelectedQuad.QuadId && args.Confirmed)
            {
                this.Notifications.text = "Quad selection confirmed.";
                this.Notifications.gameObject.SetActive(true);
                this.OkView.interactable = true;
            }
            else
            {
                this.Notifications.text = "Quad in use please select another.";
                this.Notifications.gameObject.SetActive(true);
                this.OkView.interactable = false;
            }
            
        }

        private void ActiveQuadSignalRNotifier_ActiveQuads(ActiveQuadsUpdate e)
        {
            if (e.ActiveQuads.Any())
            {

                this.SelectQuad.options.Clear();
                this.activeQuads.Clear();
                this.activeQuads.AddRange(e.ActiveQuads);
                
                this.SelectQuad.options.AddRange(
                    e.ActiveQuads.Select
                    (quad => new Dropdown.OptionData()
                    {
                        text = quad.QuadId
                    }));

                this.SelectQuad.interactable = true;
            }
            else
            {
                this.SelectQuad.options.Clear();
                this.activeQuads.Clear();
                this.SelectQuad.interactable = false;
            }
        }

        public override void Activate()
        {

            base.Activate();

        }

        public override void Disable()
        {
            base.Disable();
        }

        public override void Complete()
        {
            base.Complete();
        }

        private void ProcessSelection(int selectedQuad)
        {
            //Map the selected quad.
            this.userSelectedQuad = this.activeQuads[selectedQuad];

            //Enable confirm Selection button
            this.ConfirmSelection.interactable = true;
        }

        void Update()
        {
            if (this.activeQuads.Any())
            {
                this.SelectQuad.interactable = true;
                this.Notifications.gameObject.SetActive(false);
            }
            else
            {
                this.SelectQuad.interactable = false;
                this.Notifications.text = "Waiting for active quads.........";
                this.Notifications.gameObject.SetActive(true);
            }
        }
    }
}
