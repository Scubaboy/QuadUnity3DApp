using Assets.MonoBehaviour_Extensions.SignalR.Container_Events.ActiveQuadContainer;
using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Interfaces.SignalRClientContainers
{
    public interface ISignalRActiveQuadContainer
    {
        event ActiveQuadsUpdateEventHandler ActiveQuads;
        void ConfirmQuadSelection(ActiveQuad selectedQuad);
        event QuadSelectionConfirmedEventHandler QuadSelectionConfirmed;
    }
}
