using Assets.Services.Interfaces;
using Assets.Services.SignalR.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.MsgToClass
{
    public class MsgParamsToClass : ISignalRClientParamToClass
    {
        public object Convert(string methodParams, Type classType)
        {
           return JsonConvert.DeserializeObject(methodParams, classType);
        }
    }
}
