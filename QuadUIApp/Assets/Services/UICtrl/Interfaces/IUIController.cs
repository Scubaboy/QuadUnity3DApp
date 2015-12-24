using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.UICtrl.Interfaces
{
    public interface IUIController
    {
        void DisplayUIForMode(IModeTransition modeTransObj);

        void RegisterUIForMode(Modes mode, IUIMode theUI);
    }
}
