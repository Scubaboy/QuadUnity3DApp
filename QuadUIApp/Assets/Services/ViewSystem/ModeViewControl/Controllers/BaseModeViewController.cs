using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;
using Assets.Services.ViewSystem.View.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Services.ViewSystem.ModeViewControl.Controllers
{
    public class BaseModeViewController : MonoBehaviour, IModeViewController
    {
        protected List<IViewController> registeredUIControllers = new List<IViewController>();
        protected Dictionary<Modes, IView> modeUIMapping;

        public void ActivateView(IModeTracking modeTracker, IViewController viewController)
        {
            if (this.registeredUIControllers != null && this.modeUIMapping != null)
            {
                if (this.registeredUIControllers.Contains(viewController))
                {
                    if (this.modeUIMapping.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeUIMapping[modeTracker.ActiveMode].Activate();
                    }
                }
            }
        }

        public void DiactiveView(IModeTracking modeTracker, IViewController viewController)
        {
            if (this.registeredUIControllers != null && this.modeUIMapping != null)
            {
                if (this.registeredUIControllers.Contains(viewController))
                {
                    if (this.modeUIMapping.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeUIMapping[modeTracker.ActiveMode].Disable();
                    }
                }
            }
        }

        public void RegisterToControlView(IViewController viewController)
        {
            if (this.registeredUIControllers !=null)
            {
                if (!this.registeredUIControllers.Contains(viewController))
                {
                    this.registeredUIControllers.Add(viewController);
                }
            }
        }
    }
}
