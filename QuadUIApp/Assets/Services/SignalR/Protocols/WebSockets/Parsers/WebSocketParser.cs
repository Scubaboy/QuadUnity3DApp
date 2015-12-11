using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Protocols.WebSockets.Parsers
{
    internal static class WebSocketParser
    {
        public static wsMessageType ReceivedMessagetype(string data)
        {
            var messageType = wsMessageType.HubToClient;

            if (data.Length == 2 && data[0] == '{' && data[1] == '}')
            {
                messageType = wsMessageType.KeepAlive;
            }

            return messageType;
        }
    }
}
