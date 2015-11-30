namespace Assets.Services.SignalR.Models
{
    /// <summary>
    /// Defines the structure of a Signalr hub to client message.
    /// </summary>
    internal class HubClientMessageWrapper
    {
        /// <summary>
        /// Initializes the class to empty results;
        /// </summary>
        public HubClientMessageWrapper()
        {
            this.Hubname = string.Empty;
            this.ClientMethodName = string.Empty;
            this.ClientMethodParameters = string.Empty;
        }

        /// <summary>
        /// Gets or sets the hubname.
        /// </summary>
        public string Hubname { get; set; }
    
        /// <summary>
        /// Gets or sets the requested client method to execute. 
        /// </summary>
        public string ClientMethodName { get; set; }
        
        /// <summary>
        /// Gets or sets the client method parameter string received from the signalr hub.
        /// </summary>
        public string ClientMethodParameters { get; set; }
    }
}
