using Assets.Services.ViewSystem.ViewControl.Interfaces;

namespace Assets.Services.ViewSystem.ModeControl.Interfaces
{
    interface IModeTrackingUpdateModeRegister
    {
        void RegisterToUpdate(IViewController updater);
    }
}
