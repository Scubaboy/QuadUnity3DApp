using Assets.Services.UICtrl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.Services.ModeCtrl;
using Assets.Services.ModeCtrl.Interfaces;
using Assets.Services.UIs.Interfaces;

namespace Assets.Services.UICtrl.Controllers
{
    public class UIController : MonoBehaviour, IUIController
    {
        protected Dictionary<Modes, IUIMode> modeUIMapping;
        protected Dictionary<Modes, ModeMapping> NextMode;

        public void DisplayUIForMode(IModeTransition modeTransObj)
        {
            switch(modeTransObj.Status)
            {
                case ExecutionStatus.Canceled:
                    {
                        this.modeUIMapping[modeTransObj.ActiveMode].Disable();
                        this.modeUIMapping[NextMode[modeTransObj.ActiveMode].Canceled].Activate();
                        modeTransObj.ActiveMode = NextMode[modeTransObj.ActiveMode].Canceled;
                        break;
                    }
                case ExecutionStatus.Complete:
                    {
                        this.modeUIMapping[modeTransObj.ActiveMode].Disable();
                        this.modeUIMapping[NextMode[modeTransObj.ActiveMode].Completed].Activate();
                        modeTransObj.ActiveMode = NextMode[modeTransObj.ActiveMode].Completed;
                        break;
                    }
                case ExecutionStatus.Requested:
                    {
                        this.modeUIMapping[modeTransObj.ActiveMode].Activate();
                        modeTransObj.Status = ExecutionStatus.Running;
                        break;
                    }
            }
        }

        public void RegisterUIForMode(Modes mode, IUIMode theUI)
        {
            if (this.modeUIMapping != null)
            {
                if (!this.modeUIMapping.ContainsKey(mode))
                {
                    this.modeUIMapping.Add(mode, theUI);
                }
            }
            else
            {
                throw new UIAlreadyMappedToModeException();
            }
        }
    }
}
