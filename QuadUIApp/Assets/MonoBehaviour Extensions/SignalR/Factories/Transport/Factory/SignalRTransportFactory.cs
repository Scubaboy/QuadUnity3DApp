namespace Assets.MonoBehaviour_Extensions.SignalR.Factories.Transport.Factory
{
    using Assets.Services.Interfaces;
    using Services.SignalR.Protocols.WebSockets;
    using System;
    using UnityEngine;


    public class SignalRTransportFactory : MonoBehaviour, ISignalRTransportFactory
    {
        public ISignalRTransportCtrl CreateWebSocketTransport(string hubName, string signalRHost, bool useSecure = true)
        {
            return new SignalRTransportWS(hubName, signalRHost, useSecure);
        }
    }
}
