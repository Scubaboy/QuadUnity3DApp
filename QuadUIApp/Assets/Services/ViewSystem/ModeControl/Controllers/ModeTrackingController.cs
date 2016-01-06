using Assets.Services.ViewSystem.ModeControl.Exceptions;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.View.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services.ViewSystem.ModeControl.Controllers
{
    public class ModeTrackingController : MonoBehaviour, IModeTracking, IModeTrackingUpdateMode, IModeTrackingUpdateModeRegister, 
        IModeTrackingUpdateStatus, IModeTrackingUpdateStatusRegister
    {
        private List<IViewController> registeredControllers = new List<IViewController>();

        private List<IView> registeredModeControllers = new List<IView>();

        public Modes ActiveMode
        {
          get; protected set;
        }

        public ExecutionStatus Status
        {
            get; private set;
        }

        public void ChangeActiveMode(Modes newMode, IViewController updater)
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

        public void ChangeActiveModeStatus(ExecutionStatus status, IView updater)
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

        public void RegisterToUpdate(IView updater)
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

        public void RegisterToUpdate(IViewController updater)
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
