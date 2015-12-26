using Assets.Services.UICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    interface IModeTrackingUpdateModeRegister
    {
        void RegisterToUpdate(IUIController updater);
    }
}
