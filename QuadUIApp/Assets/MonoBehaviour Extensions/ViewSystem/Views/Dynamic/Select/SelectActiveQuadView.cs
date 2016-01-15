namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Select
{
    using SignalR.Interfaces.SignalRClientContainers;
    using Services.ViewSystem.View.Controller;
    using UnityEngine.UI;
    using SignalR.Clients;
    using SignalR.Container_Events.ActiveQuadContainer;
    using System.Linq;
    using System.Collections.Generic;
    using Services.Models;

    public class SelectActiveQuadView : BaseViewController 
    {
        public Button ConfirmSelection;
        public Button CancelView;
        public Dropdown SelectQuad;
        public Button OkView;
        public Text Notifications;
        public Text SelectionBoxHeader;
        private ISignalRActiveQuadContainer activeQuadSignalRNotifier;
        private List<ActiveQuad> activeQuads;
        private ActiveQuad userSelectedQuad;
        private bool waitingSelectedQuadConfirmation;

        public override void Awake()
        {
            base.Awake();

            //Register with the ActiveQuad SignalR container.
            this.activeQuadSignalRNotifier = FindObjectOfType<ActiveQuadSignalRClient>()
                .GetComponent <ActiveQuadSignalRClient>() as ISignalRActiveQuadContainer;

            this.activeQuadSignalRNotifier.ActiveQuads += ActiveQuadSignalRNotifier_ActiveQuads;
            this.activeQuadSignalRNotifier.QuadSelectionConfirmed += ActiveQuadSignalRNotifier_QuadSelectionConfirmed;
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
            this.activeQuadSignalRNotifier.ConfirmQuadSelection(this.userSelectedQuad);

            this.waitingSelectedQuadConfirmation = true;
        }

        private void ActiveQuadSignalRNotifier_QuadSelectionConfirmed(object sender, QuadSelectionConfirmedEventArgs args)
        {
            if (args.TheQuad.QuadId == this.userSelectedQuad.QuadId && args.Confirmed)
            {
                
            }
        }

        private void ActiveQuadSignalRNotifier_ActiveQuads(object sender, ActiveQuadsUpdateEventArgs e)
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
            if (this.waitingSelectedQuadConfirmation)
            {

            }

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
