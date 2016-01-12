using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Container_Events.ActiveQuadContainer
{
    public class QuadSelectionConfirmedEventArgs : EventArgs
    {

        public QuadSelectionConfirmedEventArgs(ActiveQuad theQuad, bool confirmed)
        {
            this.TheQuad = theQuad;
            this.Confirmed = confirmed;
        }

        public bool Confirmed
        {
            get; private set;
        }

        public ActiveQuad TheQuad
        {
            get; private set;
        }
    }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public delegate void QuadSelectionConfirmedEventHandler(object sender, QuadSelectionConfirmedEventArgs args);
    
}
