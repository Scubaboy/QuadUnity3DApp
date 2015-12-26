using Assets.Services.UICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    public interface IModeTrackingUpdateMode : IModeTracking
    {
        void ChangeActiveMode(Modes newMode, IUIController updater);
    }
}
