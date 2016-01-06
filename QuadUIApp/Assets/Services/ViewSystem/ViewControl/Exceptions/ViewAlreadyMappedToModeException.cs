using System;

namespace Assets.Services.ViewSystem.ViewControl.Exceptions
{
    public class ViewAlreadyMappedToModeException : Exception
    {
        public ViewAlreadyMappedToModeException(string message) : base(message)
        {

        }
    }
}
