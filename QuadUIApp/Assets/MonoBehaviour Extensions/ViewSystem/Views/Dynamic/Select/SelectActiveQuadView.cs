namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Select
{
    using SignalR.Interfaces.SignalRClientContainers;
    using Services.ViewSystem.View.Controller;
    using UnityEngine.UI;
    using SignalR.Clients;
    using SignalR.Container_Events.ActiveQuadContainer;
    using System.Linq;
    using System.Collections.Generic;
    using Services.SignalR.Models;

    public class SelectActiveQuadView : BaseViewController 
    {
        public Button ConfirmSelection;
        public Button CancelView;
        public Dropdown SelectQuad;
        private ISignalRActiveQuadContainer activeQuadSignalRNotifier;
        private List<ActiveQuad> activeQuads;

        public override void Awake()
        {
            base.Awake();

            //Setp local stores
            this.activeQuads = new List<ActiveQuad>();
            this.SelectQuad.options = new List<Dropdown.OptionData>();

            //Register with the ActiveQuad SignalR container.
            this.activeQuadSignalRNotifier = FindObjectOfType<ActiveQuadSignalRClient>()
                .GetComponent <ActiveQuadSignalRClient>() as ISignalRActiveQuadContainer;

            this.activeQuadSignalRNotifier.ActiveQuads += ActiveQuadSignalRNotifier_ActiveQuads;
            

            this.SelectQuad.onValueChanged.RemoveAllListeners();
            this.SelectQuad.onValueChanged.AddListener(this.ProcessSelection);
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

            //Enable confirm Selection button
            this.ConfirmSelection.interactable = true;
        }
    }
}
