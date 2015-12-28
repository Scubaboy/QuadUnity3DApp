using Assets.Services.ModeCtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Services.UICtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using Assets.Services.ModeCtrl.Exceptions;

namespace Assets.Services.ModeCtrl.Controllers
{
    public class ModeTrackingController : IModeTracking, IModeTrackingUpdateMode, IModeTrackingUpdateModeRegister, 
        IModeTrackingUpdateStatus, IModeTrackingUpdateStatusRegister
    {
        private List<IUIController> registeredControllers = new List<IUIController>();

        private List<IUIMode> registeredModeControllers = new List<IUIMode>();

        public Modes ActiveMode
        {
          get; private set;
        }

        public ExecutionStatus Status
        {
            get; private set;
        }

        public void ChangeActiveMode(Modes newMode, IUIController updater)
        {
            //Set the modes status to Requested;
            if (this.registeredControllers.Contains(updater))
            {
                this.ActiveMode = newMode;
                this.Status = ExecutionStatus.Requested;
            }
            else
            {
                throw new UICtrlNotRegisteredException("UI controller " + updater.ToString() +" not registerd");
            }
        }

        public void ChangeActiveModeStatus(ExecutionStatus status, IUIMode updater)
        {
            if (this.registeredModeControllers.Contains(updater))
            {
                this.Status = status;
            }
            else
            {
                throw new ViewCtrlNotRegisteredException("View controller "  + updater.ToString() +" not regsitered");
            }
        }

        public void RegisterToUpdate(IUIMode updater)
        {
            if (!this.registeredModeControllers.Contains(updater))
            {
                this.registeredModeControllers.Add(updater);
            }
            else
            {
                throw new ViewCtrlAlreadyRegisteredException("View controller " + updater.ToString() + " already resgistere");
            }
        }

        public void RegisterToUpdate(IUIController updater)
        {
            if (!this.registeredControllers.Contains(updater))
            {
                this.registeredControllers.Add(updater);
            }
            else
            {
                throw new UICtrlAlreadyRegisteredException("UI controller " + updater.ToString() +" already registered");
            }
        }
    }
}
