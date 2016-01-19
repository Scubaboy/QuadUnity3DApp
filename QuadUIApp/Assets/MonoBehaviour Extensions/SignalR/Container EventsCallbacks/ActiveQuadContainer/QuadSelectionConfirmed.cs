using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Container_EventsCallbacks.ActiveQuadContainer
{
    public class QuadSelectionConfirmed
    {

        public QuadSelectionConfirmed(ActiveQuad theQuad, bool confirmed)
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
        public delegate void QuadSelectionConfirmedHandler(QuadSelectionConfirmed args);
    
}
