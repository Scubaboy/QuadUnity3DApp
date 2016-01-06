namespace Assets.Services.ViewSystem.View.Interfaces
{
    public interface IView
    {
        //Change status to running in the active method.
        void Activate();

        void Disable();

        void Complete();

        bool IsVisible { get; }

    }
}
