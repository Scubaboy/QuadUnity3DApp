using Assets.Services.ModeUICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UICtrl.Interfaces;
using UnityEngine;
using Assets.Services.ModeCtrl;
using Assets.Services.UIs.Interfaces;

namespace Assets.Services.ModeUICtrl.Controllers
{
    public class ModeUIController : MonoBehaviour, IModeUIController
    {
        protected List<IUIController> registeredUIControllers;
        protected Dictionary<Modes, IUIMode> modeUIMapping;

        public void ActivateUI(IModeTracking modeTracker, IUIController uiController)
        {
            if (this.registeredUIControllers != null && this.modeUIMapping != null)
            {
                if (this.registeredUIControllers.Contains(uiController))
                {
                    if (this.modeUIMapping.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeUIMapping[modeTracker.ActiveMode].Activate();
                    }
                }
            }
        }

        public void DiactiveUI(IModeTracking modeTracker, IUIController uiController)
        {
            if (this.registeredUIControllers != null && this.modeUIMapping != null)
            {
                if (this.registeredUIControllers.Contains(uiController))
                {
                    if (this.modeUIMapping.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeUIMapping[modeTracker.ActiveMode].Disable();
                    }
                }
            }
        }

        public void RegisterToControlUI(IUIController uiController)
        {
            if (this.registeredUIControllers !=null)
            {
                if (!this.registeredUIControllers.Contains(uiController))
                {
                    this.registeredUIControllers.Add(uiController);
                }
            }
        }
    }
}
