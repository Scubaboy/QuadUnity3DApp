using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.MsgParser.jsonParser
{
    public abstract class Base
    {
        public abstract object Execute(string msg);
    }
}
