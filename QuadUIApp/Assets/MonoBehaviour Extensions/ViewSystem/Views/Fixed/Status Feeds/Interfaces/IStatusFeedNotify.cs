using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Status_Feeds.Interfaces
{
    public interface IStatusFeedNotify
    {
        void PostNotification(string notification, NotificationType severety);
    }
}
