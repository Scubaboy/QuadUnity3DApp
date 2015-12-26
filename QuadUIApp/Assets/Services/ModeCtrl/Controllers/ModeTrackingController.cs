using Assets.Services.ModeCtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Services.UICtrl.Interfaces;
using Assets.Services.UIs.Interfaces;

namespace Assets.Services.ModeCtrl.Controllers
{
    public class ModeTrackingController : IModeTracking, IModeTrackingUpdateMode, IModeTrackingUpdateModeRegister, 
        IModeTrackingUpdateStatus, IModeTrackingUpdateStatusRegister
    {

        public Modes ActiveMode
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public ExecutionStatus Status
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void ChangeActiveMode(Modes newMode, IUIController updater)
        {
            //Set the modes status to Requested;
            throw new NotImplementedException();
        }

        public void ChangeActiveModeStatus(ExecutionStatus status, IUIMode updater)
        {
            throw new NotImplementedException();
        }

        public void RegisterToUpdate(IUIMode updater)
        {
            throw new NotImplementedException();
        }

        public void RegisterToUpdate(IUIController updater)
        {
            throw new NotImplementedException();
        }
    }
}
