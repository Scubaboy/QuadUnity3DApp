namespace Assets.Services.Interfaces
{
    /// <summary>
    /// SignalR client controller definition.
    /// </summary>
    public interface ISignalRClientCtrl
    {
        /// <summary>
        /// Adds a new SignalR client to the controllers poll of clients.
        /// </summary>
        /// <param name="client">Client.</param>
        void Connect(ISignalRClient client);
    }
}
