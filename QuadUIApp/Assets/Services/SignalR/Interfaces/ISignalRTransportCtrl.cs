using Assets.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.SignalR.Interfaces
{
    public interface ISignalRTransportCtrl
    {
        bool Connect();

        void PostToSendQueue(string msg);

        List<ReceivedSignalRMsg> Read();

        void Close();

        /// <summary>
        /// Called each update cycle of the 3D engine.
        /// </summary>
        void Update();
    }
}
