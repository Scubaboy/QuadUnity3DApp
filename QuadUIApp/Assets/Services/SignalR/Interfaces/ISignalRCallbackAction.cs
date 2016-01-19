using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Interfaces
{
    public interface ISignalRCallbackAction
    {
        T Func<T>(T param);
    }
}
