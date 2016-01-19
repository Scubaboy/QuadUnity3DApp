using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Exceptions.SignalRClientExceptions;
using UnityEngine;
using Assets.Services.SignalR.MsgParser.JsonParser;
using Assets.Services.SignalR.MsgParser;
using Assets.Services.Models;
using Assets.Services.SignalR.Interfaces;

namespace Assets.Services.SignalR.Client
{
    /// <summary>
    /// Defines the structure of a SignalR Client;
    /// </summary>
    public class SignalRClient : MonoBehaviour, ISignalRClient, ISignalRClientConnect
    {
        /// <summary>
        /// 
        /// </summary>
        protected class UnTypedActionContainer
        {
            public Type ActionType { get; set; }
            public Action<System.Object> Action { get; set; }
        };

        /// <summary>
        /// Callback map.
        /// </summary>
        private Dictionary<string, List<UnTypedActionContainer>> hubMethodCallBacks = new Dictionary<string, List<UnTypedActionContainer>>();

        /// <summary>
        /// Client to hub message send queue.
        /// </summary>
        private List<string> msgSendQueue = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        private Dictionary<Type, string> hubMethodTypeMapping;

        /// <summary>
        /// 
        /// </summary>
       // private Dictionary<string, Type> clientMethodTypeMapping;

        private Dictionary<string, ISignalRMsgParserJson> clientMethodTypeMapping;

        /// <summary>
        /// 
        /// </summary>
        private ISignalRMsgParserJson hubToClientMsgParser;

        /// <summary>
        /// 
        /// </summary>
        private HubConnectionParams hubConnectionParams;
        
        /// <summary>
        /// Gets the attached hub name.
        /// </summary>
        public string HubName
        {
            get
            {
                return this.hubConnectionParams.HubName;
            }
        }

        /// <summary>
        /// Gets the SignalR host server url.
        /// </summary>
        public string HostServerUrl
        {
            get
            {
                return this.hubConnectionParams.HostServerUrl;
            }
        }

        /// <summary>
        /// Defines if a secure connection is to be used. 
        /// </summary>
        public bool UseSecureConnection
        {
            get
            {
                return this.hubConnectionParams.UseSecureConnection;
            }
        }

        public Dictionary<Type, string> HubMethodTypeMapping
        {
            set
            {
                this.hubMethodTypeMapping = value;
            }
        }

        public Dictionary<string, ISignalRMsgParserJson> ClientMethodTypeMapping
        {
            set
            {
                this.clientMethodTypeMapping = value;
            }
        }

        public HubConnectionParams HubConnectionParams
        {
            set
            {
                this.hubConnectionParams = value;
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
        
        public void MsgRcved(List<ReceivedSignalRMsg> rcvedMsgs)
        {
            rcvedMsgs.ForEach(msg =>
            {
                if (msg.Msg.ContainsHubName(this.hubConnectionParams.HubName))
                {
                    //Try and extract the method name
                    var methodName = msg.Msg.GetMethodName();

                    if (methodName != string.Empty)
                    {
                        //Extract the msg parser if one has been defined.
                        if (this.clientMethodTypeMapping.ContainsKey(methodName))
                        {

                            //Extract the client method content.
                            this.clientMethodTypeMapping[methodName].ParseHubToClientMsg(msg.Msg);

                            this.hubMethodCallBacks[methodName].ForEach(callback =>
                            {
                                callback.Action(this.clientMethodTypeMapping[methodName].Parameters);
                            });

                        }
                    }
                }
            });
            
        }

        public void Register<T>(string methodName, Action<T> callback) where T : class
        {
            var test = new HubToClientMsgParserJson<T>();

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
                throw new SignalRClientMethodRegisterUnknownException(string.Format("Method name {0} not supported by client", methodName));
            }
        }

        public void Send<T>(T msg) where T : class
        {
            if (this.hubMethodTypeMapping.ContainsKey(msg.GetType()))
            {
                //Build the message
                //{ "H":"chathub","M":"Send","A":["tester","hello"],"I":0}
                var intermsg = string.Format("\"H\":\"{0}\",\"M\":\"{1}\",\"A\":[{2}],\"I\":0",
                    this.hubConnectionParams.HubName,
                    this.hubMethodTypeMapping[msg.GetType()],
                    msg.ToString());

                this.msgSendQueue.Add("{" + intermsg +"}");
            }
            else
            {
                throw new SignalRClientUnSupportedSendMsgTypeException(
                    string.Format(
                        "Message type {0} not supported by client for hub {1}", 
                        msg.GetType(),
                        this.hubConnectionParams.HubName));
            }
        }
    }
}
