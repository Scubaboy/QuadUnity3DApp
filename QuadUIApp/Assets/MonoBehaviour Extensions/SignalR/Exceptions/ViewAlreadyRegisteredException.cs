using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Exceptions
{
    internal class ViewAlreadyRegisteredException : Exception
    {
        public ViewAlreadyRegisteredException(string message) : base(message)
        {

        }
    }
}
