using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Exceptions.SignalRClientExceptions
{
    public class SignalRClientMethodRegisterUnknownException : Exception
    {
        public SignalRClientMethodRegisterUnknownException(string message) : base(message)
        {

        }
    }
}
