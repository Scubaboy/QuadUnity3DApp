using System;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Controllers;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.View.Interfaces;
using UnityEngine;

namespace Assets.Services.ViewSystem.View.Controller
{
    public  class BaseViewController : MonoBehaviour, IView
    {
        private IModeTrackingUpdateStatus modeStatusUpdate;

        private IModeTrackingUpdateStatusRegister modeStatusUpdateRegister;

        private bool isActive = false;

        public bool IsVisible
        {
            get
            {
                return this.isActive;
            }

            protected set
            {
                this.isActive = value;
                gameObject.SetActive(value);
            }
        }

        public virtual void Awake()
        {
            //Get the mode tracking object.
            var modeTrackingController = FindObjectOfType<ModeTrackingController>()
                .GetComponent<ModeTrackingController>();

            this.modeStatusUpdate = modeTrackingController as IModeTrackingUpdateStatus;
            this.modeStatusUpdateRegister = modeTrackingController as IModeTrackingUpdateStatusRegister;

            //Register to update the mode status.
            this.modeStatusUpdateRegister.RegisterToUpdate(this);
        }

        void Start()
        {
            this.IsVisible = false;
        }

        public virtual void Activate()
        {
            if (this.modeStatusUpdate != null)
            {
                this.modeStatusUpdate.ChangeActiveModeStatus(ExecutionStatus.Running, this);
                this.IsVisible = true;
            }
        }

        public virtual void Disable()
        {
            if (this.modeStatusUpdate != null)
            {
                this.modeStatusUpdate.ChangeActiveModeStatus(ExecutionStatus.Canceled, this);
                this.IsVisible = false;
            }
        }

        public virtual void Complete()
        {
            if (this.modeStatusUpdate != null)
            {
                this.modeStatusUpdate.ChangeActiveModeStatus(ExecutionStatus.Complete, this);
                this.IsVisible = false;
            }
        }
    }
}
