using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.UICtrl.Exceptions
{
    public class UIAlreadyMappedToModeException : Exception
    {
        public UIAlreadyMappedToModeException(string message) : base(message)
        {

        }
    }
}
