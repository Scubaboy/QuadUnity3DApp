using Assets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Assets.Services.SignalR.Models;

namespace Assets.Services.SignalR.Client
{
    /// <summary>
    /// Defines the structure of a SignalR Client;
    /// </summary>
    public class SignalRClient : ISignalRClient, ISignalRClientConnect
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
        /// Client to hub message send queue.
        /// </summary>
        private List<string> msgSendQueue;

        private Dictionary<Type, string> hubMethodTypeMapping;

        Dictionary<string, Type> clientMethodTypeMapping;

        private string hubName;

        /// <summary>
        /// Creates an instance of the SignalRClient.
        /// </summary>
        /// <param name="hubMethodTypeMapping">List of hub methods to parameter type map.</param>
        /// <param name="clientMethods">List of client methods.</param>
        /// <param name="hubName">Attached hub name.</param>
        public SignalRClient(Dictionary<Type,string> hubMethodTypeMapping, Dictionary<string, Type> clientMethodTypeMapping, string hubName)
        {
            this.hubMethodTypeMapping = hubMethodTypeMapping;
            this.clientMethodTypeMapping = clientMethodTypeMapping;
            this.hubName = hubName;
        }

        /// <summary>
        /// Gets the attached hub name.
        /// </summary>
        public string HubName
        {
            get
            {
                return this.hubName;
            }
        }

        public string GetNextMsgToSend()
        {
            if (this.msgSendQueue.Any())
            {
                var msgToSend = this.msgSendQueue.Last();
                this.msgSendQueue.Remove(msgToSend);
                return msgToSend;
            }

            return string.Empty;
        }

        public void MsgRcved(string msg)
        {
            var hubCheck = "\"H\":\"" + this.hubName + "\"";

            if (msg.Contains(hubCheck))
            {
                //Extract the client method content.
                var clientMethodContent = HubToClientMsgParser.ParseHubToClientMsg(msg);

                if (this.clientMethodTypeMapping.ContainsKey(clientMethodContent.ClientMethodName))
                {
                    var parameters = JsonConvert.DeserializeObject(clientMethodContent.ClientMethodParameters, this.clientMethodTypeMapping[clientMethodContent.ClientMethodName]);
                    this.hubMethodCallBacks[clientMethodContent.ClientMethodName].ForEach(callback =>
                    {
                        callback.Action(parameters);
                    });
                }
            }
        }

        public void Register<T>(string methodName, Action<T> callback) where T : class
        {
            if (this.clientMethodTypeMapping.ContainsKey(methodName))
            {
                if (this.hubMethodCallBacks.ContainsKey(methodName))
                {
                    this.hubMethodCallBacks[methodName].Add(new UnTypedActionContainer
                    {
                        Action = new Action<object>(x =>
                        {
                            callback(x as T);
                        })
                    });
                }
                else
                {
                    this.hubMethodCallBacks.Add(methodName, new List<UnTypedActionContainer>
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
            else
            {
                throw new SignalRClientMethodRegisterUnknownException();
            }
        }

        public void Send<T>(T msg) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
