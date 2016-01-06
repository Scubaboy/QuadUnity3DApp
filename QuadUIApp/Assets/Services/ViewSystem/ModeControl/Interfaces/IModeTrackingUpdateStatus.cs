using Assets.Services.ViewSystem.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ModeControl.Interfaces
{
    public interface IModeTrackingUpdateStatus : IModeTracking
    {
        void ChangeActiveModeStatus(ExecutionStatus status, IView updater);
    }
}
