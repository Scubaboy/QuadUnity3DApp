using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ModeViewControl.Interfaces
{
    public interface IModeViewController
    {
        void ActivateView(IModeTracking modeTracker, IViewController viewController);
        void DiactiveView(IModeTracking modeTracker, IViewController viewController);
        void RegisterToControlView(IViewController viewController);
    }
}
