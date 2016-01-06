using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ViewControl.Interfaces
{
    public interface IViewController
    {
        void UpdateDisplayViewForMode(IModeTrackingUpdateMode modeTracker, IModeViewController modeViewController);
       
    }
}
