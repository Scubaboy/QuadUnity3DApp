using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.Interfaces
{
    public interface ISignalRClientController
    {
        bool RegisterClient(ISignalRClient client);

        void Update();

        bool UnRegisterClient(ISignalRClient client);
    }
}
