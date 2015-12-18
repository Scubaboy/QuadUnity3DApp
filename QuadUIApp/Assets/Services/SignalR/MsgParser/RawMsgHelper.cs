using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.MsgParser
{
    public static class RawMsgHelper
    {
        public static bool ContainsHubName(this string msg, string hubName)
        {
            return msg.Contains("\"H\":\"" + hubName + "\"");
        }

        public static string GetMethodName(this string msg)
        {
            var hubComponent = msg.Substring(msg.IndexOf("\"H\":"));

            if (hubComponent != string.Empty)
            {
                var splitComponents = hubComponent.Split(',');

                //Should be the second component;
                if (splitComponents[1].Contains("\"M\":"))
                {
                    return splitComponents[1].Substring(splitComponents[1].IndexOf(':')).Trim('"',':'); 
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
