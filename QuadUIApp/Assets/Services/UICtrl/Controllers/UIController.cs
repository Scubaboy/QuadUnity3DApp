using Assets.Services.UICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using Assets.Services.UICtrl.Exceptions;
using Assets.Services.ModeUICtrl.Interfaces;

namespace Assets.Services.UICtrl.Controllers
{
    public class UIController : MonoBehaviour, IUIController
    { 
        protected Dictionary<Modes, ModeMapping> NextMode;

        public void UpdateDisplayUIForMode(IModeTrackingUpdateMode modeTracker, IModeUIController modeUiController)
        {
            switch(modeTracker.Status)
            {
                case ExecutionStatus.Canceled:
                    {
                        modeUiController.DiactiveUI(modeTracker, this);
                        modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Canceled, this);
                       
                        break;
                    }
                case ExecutionStatus.Complete:
                    {
                        modeUiController.DiactiveUI(modeTracker, this);
                        modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Completed, this);
                       
                        break;
                    }
                case ExecutionStatus.Requested:
                    {
                        modeUiController.ActivateUI(modeTracker, this);
                        break;
                    }
            }
        }
    }
}
