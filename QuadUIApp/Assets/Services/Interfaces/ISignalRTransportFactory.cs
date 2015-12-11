using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    /// <summary>
    /// Defines the specification for the signalr client transport protocol.
    /// </summary>
    public interface ISignalRTransportFactory
    {
        /// <summary>
        /// Creates an instance of a websocket transport protocol.
        /// </summary>
        /// <returns></returns>
        ISignalRTransportCtrl CreateWebSocketTransport(string hubName, string signalRHost, bool useSecure = true);
    }
}
