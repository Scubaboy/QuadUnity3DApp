using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Container_Events.ActiveQuadContainer
{
    public class ActiveQuadsUpdateEventArgs : EventArgs
    {
        public ActiveQuadsUpdateEventArgs(List<ActiveQuad> activeQuads)
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

    public delegate void ActiveQuadsUpdateEventHandler (object sender, ActiveQuadsUpdateEventArgs e);
}
