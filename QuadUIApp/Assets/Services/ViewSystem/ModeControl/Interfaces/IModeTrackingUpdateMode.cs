using Assets.Services.ViewSystem.ViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ModeControl.Interfaces
{
    public interface IModeTrackingUpdateMode : IModeTracking
    {
        void ChangeActiveMode(Modes newMode, IViewController updater);
    }
}
