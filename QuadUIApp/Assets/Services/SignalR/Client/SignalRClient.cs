﻿using Assets.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Assets.Services.SignalR.Models;
using Assets.Exceptions.SignalRClientExceptions;
using UnityEngine;

namespace Assets.Services.SignalR.Client
{
    /// <summary>
    /// Defines the structure of a SignalR Client;
    /// </summary>
    public class SignalRClient : MonoBehaviour, ISignalRClient, ISignalRClientConnect
    {
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
        private List<string> msgSendQueue;

        private Dictionary<Type, string> hubMethodTypeMapping;

        private Dictionary<string, Type> clientMethodTypeMapping;

        private ISignalRMsgParser hubToClientMsgParser;

        private ISignalRClientParamToClass paramsToClass;

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

        public Dictionary<string, Type> ClientMethodTypeMapping
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

        public ISignalRClientParamToClass ParamsToClass
        {
            set
            {
                this.paramsToClass = value;
            }
        }

        public ISignalRMsgParser HubToClientMsgParser
        {
            set
            {
                this.hubToClientMsgParser = value;
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
            var hubCheck = "\"H\":\"" + this.hubConnectionParams.HubName + "\"";

            rcvedMsgs.ForEach(msg =>
            {
                if (msg.Msg.Contains(hubCheck))
                {
                    //Extract the client method content.
                    var clientMethodContent = this.hubToClientMsgParser.ParseHubToClientMsg(msg.Msg);

                    if (this.clientMethodTypeMapping.ContainsKey(clientMethodContent.ClientMethodName))
                    {
                        var parameters = this.paramsToClass.Convert(clientMethodContent.ClientMethodParameters, this.clientMethodTypeMapping[clientMethodContent.ClientMethodName]);

                        this.hubMethodCallBacks[clientMethodContent.ClientMethodName].ForEach(callback =>
                        {
                            callback.Action(parameters);
                        });
                    }
                }
            });
            
        }

        public void Register<T>(string methodName, ISignalRCallbackAction callback) where T : class
        {
            if (this.clientMethodTypeMapping.ContainsKey(methodName))
            {
                if (this.hubMethodCallBacks.ContainsKey(methodName))
                {
                    this.hubMethodCallBacks[methodName].Add(new UnTypedActionContainer
                    {
                        Action = new Action<object>(x =>
                        {
                            callback.Action(x as T);
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
                            callback.Action(x as T);
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
                this.msgSendQueue.Add(string.Format("{\"H\":\"{0}\",\"M\":\"{1}\":,\"A\":{2},\"I\":0}",
                    this.hubConnectionParams.HubName,
                    this.hubMethodTypeMapping[msg.GetType()],
                    msg.ToString()));
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
