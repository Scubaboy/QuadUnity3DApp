namespace Assets.MonoBehaviour_Extensions.SignalR.Clients.ActiveQuads.Interfaces
{
    using Assets.Services.ViewSystem.View.Interfaces;

    public interface ISignalRClientViewRegistration
    {
        void RegisterView(IView ViewToRegister);
    }
}
