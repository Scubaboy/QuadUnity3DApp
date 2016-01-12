using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ModeViewControl.Interfaces
{
    public interface IModeViewController
    {
        /// <summary>
        /// 
        /// </summary>
        void ActivateFixedView(FixedViews fixedView, IViewController viewController);

        /// <summary>
        /// 
        /// </summary>
        void DisableFixedView(FixedViews fixedView, IViewController viewController);

        void ActivateDynamicView(IModeTracking modeTracker, IViewController viewController);
        void DisactiveDynamicView(IModeTracking modeTracker, IViewController viewController);
        void RegisterToControlDynamicViews(IViewController viewController);

        void RegisterToControlFixedViews(IViewController viewController);

    }
}
