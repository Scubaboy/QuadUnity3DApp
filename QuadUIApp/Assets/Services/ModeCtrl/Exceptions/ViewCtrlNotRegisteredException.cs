using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Exceptions
{
    public class ViewCtrlNotRegisteredException : Exception
    {
        public ViewCtrlNotRegisteredException(string message) : base (message)
        {

        }
    }
}
