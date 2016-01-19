using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Interfaces
{
    public interface ISignalRMsgParserString
    {
        HubClientMessageWrapper ParseHubToClientMsg(string hubMsg);
    }
}
