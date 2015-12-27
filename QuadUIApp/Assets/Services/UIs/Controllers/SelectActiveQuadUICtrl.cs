using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UIs.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Services.UIs.Controllers
{
    public abstract class SelectActiveQuadUICtrl : MonoBehaviour, IUIMode
    {
        protected IModeTrackingUpdateStatus modeStatusUpdate;

        public bool IsVisible
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual void Activate()
        {
            if (this.modeStatusUpdate != null)
            {
                this.modeStatusUpdate.ChangeActiveModeStatus(ExecutionStatus.Running, this);
            }
        }

        public abstract void Disable();
    }
}
