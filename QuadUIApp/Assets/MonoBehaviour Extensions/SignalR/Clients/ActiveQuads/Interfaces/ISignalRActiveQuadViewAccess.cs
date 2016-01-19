using Assets.MonoBehaviour_Extensions.SignalR.Clients.ActiveQuads;
using Assets.MonoBehaviour_Extensions.SignalR.Container_EventsCallbacks.ActiveQuadContainer;
using Assets.Services.Models;
using Assets.Services.ViewSystem.View.Interfaces;

namespace Assets.MonoBehaviour_Extensions.SignalR.Clients.ActiveQuads.Interfaces
{
    public interface ISignalRActiveQuadViewAccess : ISignalRClientViewRegistration
    {
        RequestQuadConfirmation ConfirmQuadSelection(IView registerdView, ActiveQuad selectedQuad);

        void RegisterActiveQuadUpdateHandler(IView registerView, ActiveQuadsUpdateHandler updateHandler);

        void RegisterConfirmedhandler(IView registerView, QuadSelectionConfirmedHandler selectionConfirmedHandler);

    }
}
