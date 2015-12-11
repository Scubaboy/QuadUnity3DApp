namespace Assets.Services.SignalR.Models
{
    /// <summary>
    /// Defines the parameters used to create a connection to a SignalR hub.
    /// </summary>
    public class HubConnectionParams
    {
        /// <summary>
        /// Creates an instance of the HubConnectionParams class
        /// </summary>
        /// <param name="hubName">SignalR hubName</param>
        /// <param name="hostServerUrl">URL of the host server.</param>
        /// <param name="useSecure">Defines if a secure connection should be used.</param>
        public HubConnectionParams (string hubName, string hostServerUrl, bool useSecure)
        {
            this.HubName = hubName;
            this.HostServerUrl = hostServerUrl;
            this.UseSecureConnection = useSecure;
        }

        /// <summary>
        /// SignalR hubName
        /// </summary>
        public string HubName
        {
            get; private set;
        }

        /// <summary>
        /// URL of the host server.
        /// </summary>
        public string HostServerUrl
        {
            get; private set;
        }

        /// <summary>
        /// Defines if a secure connection should be used.
        /// </summary>
        public bool UseSecureConnection
        {
            get; private set;
        }

    }
}
