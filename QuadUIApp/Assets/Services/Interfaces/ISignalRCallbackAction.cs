using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRCallbackAction
    {
        void Action<T>(T param);
    }
}
