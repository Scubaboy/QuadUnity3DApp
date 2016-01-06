using Assets.Services.ViewSystem.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ModeControl.Interfaces
{
    public interface IModeTrackingUpdateStatusRegister
    {
        void RegisterToUpdate(IView updater);
    }
}
