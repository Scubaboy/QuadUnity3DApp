using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ModeCtrl.Interfaces
{
    public interface IModeController
    {
        Modes GetActiveMode();

        void Transition(IModeTransition modeTransObj);
    }
}
