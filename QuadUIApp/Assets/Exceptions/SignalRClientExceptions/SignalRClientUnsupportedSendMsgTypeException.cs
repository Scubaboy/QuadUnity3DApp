using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Exceptions.SignalRClientExceptions
{
    public class SignalRClientUnSupportedSendMsgTypeException : Exception
    {
        public SignalRClientUnSupportedSendMsgTypeException(string message) : base(message)
        {

        }
    }
}
