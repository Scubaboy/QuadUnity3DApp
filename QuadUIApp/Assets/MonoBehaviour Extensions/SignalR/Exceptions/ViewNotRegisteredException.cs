using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Exceptions
{
    internal class ViewNotRegisteredException : Exception
    {
        public ViewNotRegisteredException(string message) : base (message)
        {

        }
    }
}
