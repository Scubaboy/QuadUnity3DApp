namespace Assets.MonoBehaviour_Extensions.SignalR.Controllers
{
    using Services.Interfaces;
    using Services.SignalR.Controllers;
    using System.Collections.Generic;
    using System.Linq;

    class SignalRHubConnController : HubConnController
    {
        void Awake()
        {
            this.clientTransportMap = new Dictionary<ISignalRClient, ISignalRTransportCtrl>();
        }

        public void Update()
        {
            if (this.clientTransportMap.Any())
            {
                foreach (var trans in this.clientTransportMap.Values)
                {
                    trans.Update();
                }
            }
        }
    }
}
