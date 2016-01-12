using Assets.Services.ViewSystem.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Services.ViewSystem.ViewControl.Interfaces
{
   public interface IViewControllerRegister
    {
        void RegisterToRequestModeChange(IView viewRegisterRequest);
    }
}
