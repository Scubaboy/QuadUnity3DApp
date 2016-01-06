using System.Collections.Generic;
using UnityEngine;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ViewControl.Controllers
{
    public class BaseViewController : MonoBehaviour, IViewController
    { 
        protected Dictionary<Modes, ModeMapping> NextMode;

        public void UpdateDisplayViewForMode(IModeTrackingUpdateMode modeTracker, IModeViewController modeViewController)
        {
            switch(modeTracker.Status)
            {
                case ExecutionStatus.Canceled:
                    {
                        modeViewController.DiactiveView(modeTracker, this);
                        modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Canceled, this);
                       
                        break;
                    }
                case ExecutionStatus.Complete:
                    {
                        modeViewController.DiactiveView(modeTracker, this);
                        modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Completed, this);
                       
                        break;
                    }
                case ExecutionStatus.Requested:
                    {
                        modeViewController.ActivateView(modeTracker, this);
                        break;
                    }
            }
        }
    }
}
