using Assets.Services.SignalR.MsgParser.jsonParser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRMsgParserJson
    {
        void ParseHubToClientMsg(string hubMsg);

        object Parameters { get; }
    }
}
