using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.MsgParser.jsonParser
{
    public class HubMessageWrapper<T>
    {
        public string C { get; set; }

        public List<HubMessageClientWrapper<T>> M { get; set; }

    }
}
