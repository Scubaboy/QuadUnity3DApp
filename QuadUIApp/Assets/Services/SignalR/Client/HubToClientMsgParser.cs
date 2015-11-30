using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Client
{
    internal static class HubToClientMsgParser
    {
        public static HubClientMessageWrapper ParseHubToClientMsg(string hubMsg)
        {
            var parsedMsg = new HubClientMessageWrapper();

            //Client method content.
            var clientMethodContent = hubMsg.Substring(hubMsg.IndexOf("["), hubMsg.Length - 2);

            //Remove leading trailing brackets.
            var strippedLeadingTrailingBrackets = clientMethodContent.TrimStart('[','{').TrimEnd(']','}');
           
            //Break the message down into component parts hubname, methodname and method parameters.
            var componentParts = strippedLeadingTrailingBrackets.Split(',').ToList();

            //Extract the hubname component.
            if (componentParts[0].Contains("\"H\""))
            {
                parsedMsg.Hubname = componentParts[0].Split(':')[1].TrimStart('"').TrimEnd('"');
            }
            //Extract the client method name.
            if (componentParts[1].Contains("\"M\""))
            {
                parsedMsg.Hubname = componentParts[0].Split(':')[1].TrimStart('"').TrimEnd('"');
            }
            //Extract the client method parameters.
            if (componentParts[1].Contains("\"A\""))
            {
                parsedMsg.Hubname = componentParts[0].Split(':')[1];
            }

            if (parsedMsg.Hubname == string.Empty || parsedMsg.ClientMethodName == string.Empty || parsedMsg.ClientMethodParameters == string.Empty)
            {
                throw new SignelRMessageParseException();
            }

            return parsedMsg;
        }

    }
}
