namespace Assets.Services.Interfaces
{
    /// <summary>
    /// SignalR client definition.
    /// </summary>
    public interface ISignalRClient
    {
        /// <summary>
        /// Name of server hub client attached too.
        /// </summary>
        string HubName { get; }

        /// <summary>
        /// Processes server received messages.
        /// </summary>
        /// <param name="msg">Message recieved from server.</param>
        void MsgRcved(string msg);

        /// <summary>
        /// Returns the next message to post to the SignalR hub server.
        /// </summary>
        /// <returns>Message to post.</returns>
        string GetNextMsgToSend();
    }
}
