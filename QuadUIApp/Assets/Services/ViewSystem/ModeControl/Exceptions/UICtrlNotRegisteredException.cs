using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ModeControl.Exceptions
{
    public class UICtrlNotRegisteredException :Exception
    {
        public UICtrlNotRegisteredException(string message) : base (message)
        {

        }
    }
}
