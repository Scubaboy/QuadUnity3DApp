namespace Assets.Services.SignalR.Protocols.WebSockets
{
    using Assets.Services.Interfaces;
    using Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using WebSocketSharp;
    using System.Linq;
    using Parsers;

    public class SignalRTransportWS : ISignalRTransportCtrl
    {
        /// <summary>
        /// Client to server ping test interval in milliseconds;
        /// </summary>
        private const int ServerPingCheckInterval = 10000;


        private DateTime lastUpdateTime;

        private string hubName;

        /// <summary>
        /// connection protocol.
        /// </summary>
        private WebSocket webSocket;

        private string signalRHost;

        private string connectionToken;

        private List<ReceivedSignalRMsg> receivedMessages;

        private bool signalRServerActive;

        private List<string> SendMsgQueue;

        private bool Connected;

        private bool useSecure;

        public SignalRTransportWS(string hubName,string signalRHost, bool useSecure = true)
        {
            this.hubName = hubName;
            this.signalRHost = signalRHost;
            this.receivedMessages = new List<ReceivedSignalRMsg>();
            this.signalRServerActive = false;
            this.SendMsgQueue = new List<string>();
            this.useSecure = useSecure;
            this.Connected = false;
        }

        public bool Connect()
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(
                this.useSecure ? "https://" : "http://" + this.signalRHost + "/signalr/negotiate?connectionData=%5B%7B%22name%22%3A%22"+ this.hubName + "%22%7D%5D&clientProtocol=1.3&_=1408716619953");
            var response = (HttpWebResponse)webRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                connectionToken = Uri.EscapeDataString(JsonConvert.DeserializeObject<NegotiateResponse>(sr.ReadToEnd()).ConnectionToken);
            }
            
            webSocket = new WebSocket(this.useSecure ? "wss://" : "ws://" + this.signalRHost + "/signalr/connect?transport=webSockets&connectionToken=" + connectionToken + "&connectionData=%5B%7B%22name%22%3A%22" + this.hubName +"%22%7D%5D&tid=" + UnityEngine.Random.Range(0, 11));

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

                this.signalRServerActive = true;
                this.lastUpdateTime = DateTime.Now;
                this.Connected = true;

            }
        }

        private void WebSocket_OnOpen(object sender, EventArgs e)
        {
            
        }

        private void WebSocket_OnError(object sender, WebSocketSharp.ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private void WebSocket_OnClose(object sender, CloseEventArgs e)
        {
            
        }

        private void WebSocket_OnMessage(object sender, MessageEventArgs e)
        {
            //Need to filter out keep alive messages.
            var msqType = WebSocketParser.ReceivedMessagetype(e.Data);

            switch (msqType)
            {
                case wsMessageType.HubToClient:
                    {
                        this.receivedMessages.Add(new ReceivedSignalRMsg(DateTime.Now, e.Data));
                        break;
                    }
            }
        }

        public List<ReceivedSignalRMsg> Read()
        {
            var theMessages = new List<ReceivedSignalRMsg>();
            theMessages.AddRange(this.receivedMessages);

            this.receivedMessages.Clear();

            return theMessages;
        }

        public void PostToSendQueue(string msg)
        {
            this.SendMsgQueue.Add(msg);
        }

        public void Close()
        {
            if (this.webSocket != null)
            {
                this.webSocket.Close();
            }
        }

        public void Update()
        {
            if (this.Connected)
            {
                var timeDiff = (int)(DateTime.Now - this.lastUpdateTime).TotalMilliseconds;

               if (timeDiff >= ServerPingCheckInterval)
                {
                    //Send ping test.
                    this.SendPingTest();
                    this.lastUpdateTime = DateTime.Now;
                }

                this.SendPendingMsgs();

                
            }
        }

        private void SendPendingMsgs()
        {
            if (this.webSocket != null && this.SendMsgQueue.Any())
            {
                this.SendMsgQueue.ForEach(msg => this.webSocket.Send(msg));

                this.SendMsgQueue.Clear();
            }
        }

        private void SendPingTest()
        {
            var webRequest = (HttpWebRequest)WebRequest.Create(
               this.useSecure ? "https://" : "http://" + this.signalRHost + "/signalr/ping?connectionData=%5B%7B%22name%22%3A%22" + this.hubName + "%22%7D%5D");
            var response = (HttpWebResponse)webRequest.GetResponse();

            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var pingResponse = JsonConvert.DeserializeObject<PingResponse>(sr.ReadToEnd());

                this.signalRServerActive = true;

                if (pingResponse.Response != "pong")
                {
                    this.signalRServerActive = false;
                }
            }
        }
    }
}
