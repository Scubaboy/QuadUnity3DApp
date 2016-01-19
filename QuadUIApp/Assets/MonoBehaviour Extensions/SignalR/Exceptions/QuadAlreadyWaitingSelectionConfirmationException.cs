using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.SignalR.Exceptions
{
    internal class QuadAlreadyWaitingSelectionConfirmationException : Exception
    {
        public QuadAlreadyWaitingSelectionConfirmationException(string message) : base(message)
        {

        }
    }
}
