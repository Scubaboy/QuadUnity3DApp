namespace Assets.Services.SignalR.MsgParser
{
    using Interfaces;
    using System.Linq;
    using Models;

    public class HubToClientMsgParser : ISignalRMsgParser
    {
        public HubClientMessageWrapper ParseHubToClientMsg(string hubMsg)
        {
            var parsedMsg = new HubClientMessageWrapper();

            //Client method content.
            var clientMethodContent = hubMsg.Substring(hubMsg.IndexOf("["));

            //Remove leading trailing brackets.
            var strippedLeadingTrailingBrackets = clientMethodContent.TrimStart('[', '{').TrimEnd(']', '}');

            //Break the message down into component parts hubname, methodname and method parameters.
            var hubNameClientMethodNameString = strippedLeadingTrailingBrackets.Substring(0, strippedLeadingTrailingBrackets.IndexOf("\"A\":") -1);
            var clientMethodParameters = strippedLeadingTrailingBrackets.Substring(strippedLeadingTrailingBrackets.IndexOf("\"A\":"));

            var componentParts = hubNameClientMethodNameString.Split(',').ToList();

            //Extract the hubname component.
            if (componentParts[0].Contains("\"H\""))
            {
                parsedMsg.Hubname = componentParts[0].Split(':')[1].TrimStart('"').TrimEnd('"');
            }
            //Extract the client method name.
            if (componentParts[1].Contains("\"M\""))
            {
                parsedMsg.ClientMethodName = componentParts[1].Split(':')[1].TrimStart('"').TrimEnd('"');
            }

            
            var clientMethodParametersSplit = clientMethodParameters.Split('[');
            //Extract the client method parameters.
            if (clientMethodParametersSplit[0].Contains("\"A\""))
            {
                parsedMsg.ClientMethodParameters = '['+ clientMethodParametersSplit[1] + "}]";
            }

            if (parsedMsg.Hubname == string.Empty || parsedMsg.ClientMethodName == string.Empty || parsedMsg.ClientMethodParameters == string.Empty)
            {
                // throw new SignelRMessageParseException();
            }

            return parsedMsg;
        }
    }
}
