using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ViewControl.Interfaces
{
    public interface IViewControllerRequests
    {
        void RequestModeChange(Modes requestedMode, IView viewRegisterRequest);
    }
}
