using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.MonoBehaviour_Extensions.ViewSystem.ModeControl
{
    public class ModeController : ModeTrackingController
    {
        void Awake()
        {
            this.ActiveMode = Modes.Initialise;
        }
    }
}
