using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Exceptions
{
    public class UICtrlAlreadyRegisteredException : Exception
    {
        public UICtrlAlreadyRegisteredException(string message) : base (message)
        {

        }
    }
}
