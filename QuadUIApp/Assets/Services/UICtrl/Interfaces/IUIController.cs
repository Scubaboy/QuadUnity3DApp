using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.ModeUICtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.UICtrl.Interfaces
{
    public interface IUIController
    {
        void UpdateDisplayUIForMode(IModeTrackingUpdateMode modeTracker, IModeUIController modeUiController);
       
    }
}
