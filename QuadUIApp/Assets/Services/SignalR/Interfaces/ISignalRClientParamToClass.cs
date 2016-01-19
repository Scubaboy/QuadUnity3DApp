using System;

namespace Assets.Services.SignalR.Interfaces
{
    public interface ISignalRClientParamToClass
    {
        object Convert(string methodParams, Type classType);
    }
}
