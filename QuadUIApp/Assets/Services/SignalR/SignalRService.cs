namespace Assets.Services.SignalR
{
    using WebSocketSharp;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class SignalRService
    {
        class UnTypedActionContainer
        {
            public Type ActionType { get; set; }
            public Action<Object> Action { get; set; }
        };

        /// <summary>
        /// Callback map.
        /// </summary>
        private Dictionary<string, List<UnTypedActionContainer>> hubMethodCallBacks = new Dictionary<string, List<UnTypedActionContainer>>();

        /// <summary>
        /// Websocket instance;
        /// </summary>
        private WebSocket webSocket;

        /// <summary>
        /// SignalR generated unquie connection token id.
        /// </summary>
        private string connectionToken;


        /// <summary>
        /// Registers a listener for a hub to client send.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        /// <param name="clientMethodName">Client method name called by hub.</param>
        /// <param name="callback">Registerd callback.</param>
        public void Register<T>(string clientMethodName, Action<T> callback) where T : class
        {
            if (this.hubMethodCallBacks.ContainsKey(clientMethodName))
            {
                this.hubMethodCallBacks[clientMethodName].Add(new UnTypedActionContainer
                { 
                    Action = new Action<object>(x =>
                    {
                        callback(x as T);
                    })
                });
            }
            else
            {
                this.hubMethodCallBacks.Add(clientMethodName, new List<UnTypedActionContainer>
                {
                    new UnTypedActionContainer
                    {
                        Action = new Action<object>(x =>
                        {
                            callback(x as T);
                        })
                    }
                });
            }

        }

        public void SendMessage(string name, string message)
        {
            //{"H":"chathub","M":"Send","A":["tester","hello"],"I":0}
          /*  var payload = new RollerBallWrapper()
            {
                H = "myhub",
                M = "Send",
                A = new[] { name, message },
                I = 12
            };
            var wsPacket = JsonConvert.SerializeObject(payload);
            _ws.Send(wsPacket);*/
        }
    }
}
