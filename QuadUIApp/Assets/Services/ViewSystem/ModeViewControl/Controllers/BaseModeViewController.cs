using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;
using Assets.Services.ViewSystem.View.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Assets.Services.ViewSystem.ModeViewControl.Controllers
{
    public class BaseModeViewController : MonoBehaviour, IModeViewController
    {
        /// <summary>
        /// 
        /// </summary>
        protected List<IViewController> registeredDynamicViewsUIControllers = new List<IViewController>();

        /// <summary>
        /// 
        /// </summary>
        protected List<IViewController> registeredFixedViewsUIOControllers = new List<IViewController>();

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<Modes, IView> modeDynamicViewMappings;

        /// <summary>
        /// 
        /// </summary>
        protected Dictionary<FixedViews, IView> fixedViewsMappings;


        public void ActivateDynamicView(IModeTracking modeTracker, IViewController viewController)
        {
            if (this.registeredDynamicViewsUIControllers != null && this.modeDynamicViewMappings != null)
            {
                if (this.registeredDynamicViewsUIControllers.Contains(viewController))
                {
                    if (this.modeDynamicViewMappings.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeDynamicViewMappings[modeTracker.ActiveMode].Activate();
                    }
                }
            }
        }

        public void DisactiveDynamicView(IModeTracking modeTracker, IViewController viewController)
        {
            if (this.registeredDynamicViewsUIControllers != null && this.modeDynamicViewMappings != null)
            {
                if (this.registeredDynamicViewsUIControllers.Contains(viewController))
                {
                    if (this.modeDynamicViewMappings.ContainsKey(modeTracker.ActiveMode))
                    {
                        modeDynamicViewMappings[modeTracker.ActiveMode].Disable();
                    }
                }
            }
        }

        public void RegisterToControlDynamicViews(IViewController viewController)
        {
            if (this.registeredDynamicViewsUIControllers !=null)
            {
                if (!this.registeredDynamicViewsUIControllers.Contains(viewController))
                {
                    this.registeredDynamicViewsUIControllers.Add(viewController);
                }
            }
        }

        public void RegisterToControlFixedViews(IViewController viewController)
        {
            if (this.registeredFixedViewsUIOControllers != null)
            {
                if (!this.registeredFixedViewsUIOControllers.Contains(viewController))
                {
                    this.registeredFixedViewsUIOControllers.Add(viewController);
                }
            }
        }

        public void ActivateFixedView(FixedViews fixedView, IViewController viewController)
        {
            if (this.registeredFixedViewsUIOControllers != null && this.fixedViewsMappings != null)
            {
                if (this.registeredDynamicViewsUIControllers.Contains(viewController))
                {
                    if (this.fixedViewsMappings.ContainsKey(fixedView))
                    {
                        fixedViewsMappings[fixedView].Activate();
                    }
                }
            }
        }

        public void DisableFixedView(FixedViews fixedView, IViewController viewController)
        {
            if (this.registeredFixedViewsUIOControllers != null && this.fixedViewsMappings != null)
            {
                if (this.registeredDynamicViewsUIControllers.Contains(viewController))
                {
                    if (this.fixedViewsMappings.ContainsKey(fixedView))
                    {
                        fixedViewsMappings[fixedView].Disable();
                    }
                }
            }
        }
    }
}
