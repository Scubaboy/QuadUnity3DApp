using Assets.Services.Models;
using System.Collections.Generic;

namespace Assets.MonoBehaviour_Extensions.SignalR.Container_EventsCallbacks.ActiveQuadContainer
{
    public class ActiveQuadsUpdate
    {
        public ActiveQuadsUpdate(List<ActiveQuad> activeQuads)
        {
            this.ActiveQuads = activeQuads;
        }

        public List<ActiveQuad> ActiveQuads
        {
            get; private set;
        }
    }

    /// <summary>
    /// Delegate declaration.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    public delegate void ActiveQuadsUpdateHandler (ActiveQuadsUpdate e);
}
