using Assets.Services.ModeCtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.UIs.Interfaces
{
    public interface IUIMode
    {
        //Change status to running in the active method.
        void Activate();

        void Disable();

        bool IsVisible { get; }

    }
}
