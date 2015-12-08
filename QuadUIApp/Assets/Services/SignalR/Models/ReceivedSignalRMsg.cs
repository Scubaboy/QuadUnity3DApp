using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Models
{
    public class ReceivedSignalRMsg
    {
        public ReceivedSignalRMsg(DateTime timeReceived, string msg)
        {
            this.Msg = msg;
            this.TimeReceived = timeReceived;
        }

        public DateTime TimeReceived
        {
            get;
            private set;

        }

        public string Msg
        {
            get;
            private set; }
    }
}
