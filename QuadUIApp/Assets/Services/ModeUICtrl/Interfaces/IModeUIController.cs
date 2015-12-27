using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeUICtrl.Interfaces
{
    public interface IModeUIController
    {
        void ActivateUI(IModeTracking modeTracker, IUIController uiController);
        void DiactiveUI(IModeTracking modeTracker, IUIController uiController);
        void RegisterToControlUI(IUIController uiController);
    }
}
