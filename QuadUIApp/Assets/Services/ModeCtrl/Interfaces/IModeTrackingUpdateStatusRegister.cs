using Assets.Services.UIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    public interface IModeTrackingUpdateStatusRegister
    {
        void RegisterToUpdate(IUIMode updater);
    }
}
