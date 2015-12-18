﻿using Assets.Services.SignalR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRMsgParserString
    {
        HubClientMessageWrapper ParseHubToClientMsg(string hubMsg);
    }
}