using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ModeControl.Exceptions
{
    public class ViewCtrlAlreadyRegisteredException : Exception
    {
        public ViewCtrlAlreadyRegisteredException(string message) : base (message)
        {

        }
    }
}
