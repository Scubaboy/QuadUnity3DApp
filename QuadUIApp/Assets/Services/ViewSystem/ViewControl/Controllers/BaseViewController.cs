using System.Collections.Generic;
using UnityEngine;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;
using Assets.Services.ViewSystem.View.Interfaces;
using System;

namespace Assets.Services.ViewSystem.ViewControl.Controllers
{
    public class BaseViewController : MonoBehaviour, IViewController, IViewControllerRegister, IViewControllerRequests
    { 
        protected Dictionary<Modes, ModeMapping> NextMode;

        private List<IView> registeredViews = new List<IView>();

        private bool modeChangeRequest = false;

        private Modes modeRequested;

        public void RegisterToRequestModeChange(IView viewRegisterRequest)
        {
            if (this.registeredViews != null && !this.registeredViews.Contains(viewRegisterRequest))
            {
                this.registeredViews.Add(viewRegisterRequest);
            }
        }

        public void RequestModeChange(Modes requestedMode, IView viewRegisterRequest)
        {
            if (this.registeredViews != null && this.registeredViews.Contains(viewRegisterRequest))
            {
                modeChangeRequest = true;
                modeRequested = requestedMode;
            }
        }

        public void UpdateDisplayViewForMode(IModeTrackingUpdateMode modeTracker, IModeViewController modeViewController)
        {
            if (this.modeChangeRequest)
            {
                modeTracker.ChangeActiveMode(this.modeRequested, this);
                this.modeChangeRequest = false;
            }
            else
            {
                switch (modeTracker.Status)
                {
                    case ExecutionStatus.Canceled:
                        {
                            modeViewController.DisactiveDynamicView(modeTracker, this);
                            modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Canceled, this);

                            break;
                        }
                    case ExecutionStatus.Complete:
                        {
                            modeViewController.DisactiveDynamicView(modeTracker, this);
                            modeTracker.ChangeActiveMode(NextMode[modeTracker.ActiveMode].Completed, this);

                            break;
                        }
                    case ExecutionStatus.Requested:
                        {
                            modeViewController.ActivateDynamicView(modeTracker, this);
                            break;
                        }
                }
            }
        }
    }
}
