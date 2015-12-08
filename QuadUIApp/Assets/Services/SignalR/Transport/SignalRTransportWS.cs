namespace Assets.Services.SignalR.Transport
{
    using Assets.Services.Interfaces;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using WebSocketSharp;

    public class SignalRTransportWS : ISignalRTransportCtrl
    {
        private ISignalRClient client;

        /// <summary>
        /// connection protocol.
        /// </summary>
        private WebSocket webSocket;

        private string signalRHost;

        private string connectionToken;

        private List<ReceivedSignalRMsg> receivedMessages;

        public SignalRTransportWS(ISignalRClient client,string signalRHost)
        {
            this.client = client;
            this.signalRHost = signalRHost;
            this.receivedMessages = new List<ReceivedSignalRMsg>();
        }

        public bool Connect()
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(
                this.signalRHost + "/signalr/negotiate?connectionData=%5B%7B%22name%22%3A%22"+ this.client.HubName +"%22%7D%5D&clientProtocol=1.3&_=1408716619953");
            var response = (HttpWebResponse)webRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                connectionToken = Uri.EscapeDataString(JsonConvert.DeserializeObject<NegotiateResponse>(sr.ReadToEnd()).ConnectionToken);
            }

            //Not going to work for localhost.
            var removedWWW = this.signalRHost.Substring(this.signalRHost.IndexOf('.'));
            webSocket = new WebSocket("ws://" + removedWWW + "/signalr/connect?transport=webSockets&connectionToken=" + connectionToken + "&connectionData=%5B%7B%22name%22%3A%22" + this.client.HubName +"%22%7D%5D&tid=" + UnityEngine.Random.Range(0, 11));

            this.AttachAndConnect();

            return true;
        }

        private void AttachAndConnect()
        {
            if (this.webSocket != null)
            {
                this.webSocket.OnMessage += WebSocket_OnMessage;
                this.webSocket.OnClose += WebSocket_OnClose;
                this.webSocket.OnError += WebSocket_OnError;    
                this.webSocket.OnOpen += WebSocket_OnOpen;

                this.webSocket.Connect();
            }
        }

        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WebSocket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            //Need to filter out keep alive messages.
            this.receivedMessages.Add(new ReceivedSignalRMsg(DateTime.Now, e.Data));
        }

        public List<ReceivedSignalRMsg> Read()
        {
            var theMessages = new List<ReceivedSignalRMsg>();
            theMessages.AddRange(this.receivedMessages);

            this.receivedMessages.Clear();

            return theMessages;
        }

        public bool Send(string msg)
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            if (this.webSocket != null)
            {
                this.webSocket.Close();
            }
        }
    }
}
