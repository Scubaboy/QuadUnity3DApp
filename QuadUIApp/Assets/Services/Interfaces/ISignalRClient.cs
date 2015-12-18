namespace Assets.Services.Interfaces
{
    using Assets.Services.SignalR.Models;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// SignalR client definition.
    /// </summary>
    public interface ISignalRClient
    {
        /// <summary>
        /// Name of server hub client attached too.
        /// </summary>
        string HubName { get; }

        /// <summary>
        /// URl to the SignalR host server. 
        /// </summary>
        string HostServerUrl { get; }

        /// <summary>
        /// Defines if a secure connection is to be used. 
        /// </summary>
        bool UseSecureConnection { get; }

        /// <summary>
        /// Processes server received messages.
        /// </summary>
        /// <param name="msg">Message recieved from server.</param>
        void MsgRcved(List<ReceivedSignalRMsg> rcvedMsgs);

        /// <summary>
        /// Returns the next message to post to the SignalR hub server.
        /// </summary>
        /// <returns>Message to post.</returns>
        string GetNextMsgToSend();


        /// <summary>
        /// 
        /// </summary>
        Dictionary<Type, string> HubMethodTypeMapping
        {
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        Dictionary<string, ISignalRMsgParserJson> ClientMethodTypeMapping
        {
            set;
        }

        HubConnectionParams HubConnectionParams
        {
            set;
        }
    }
}
