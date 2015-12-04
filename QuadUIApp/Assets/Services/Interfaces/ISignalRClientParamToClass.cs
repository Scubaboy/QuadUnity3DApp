using System;

namespace Assets.Services.Interfaces
{
    public interface ISignalRClientParamToClass
    {
        object Convert(string methodParams, Type classType);
    }
}
