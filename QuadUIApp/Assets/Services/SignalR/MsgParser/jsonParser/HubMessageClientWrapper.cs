using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.MsgParser.jsonParser
{
    public class HubMessageClientWrapper<T>
    {
        public string H { get; set; }

        public string M { get; set; }

        public List<T> A { get; set; }
    }
}
