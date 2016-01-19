namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Status_Feeds
{
    using Interfaces;
    using Services.ViewSystem.View.Controller;
    using System;
    using System.Text;

    public class StatusFeedsView : BaseViewController , IStatusFeedNotify
    {
        
        public void PostNotification(string notification, NotificationType severety)
        {
            throw new NotImplementedException();
        }

        protected override void ConfigureView()
        {
            
        }
    }
}
