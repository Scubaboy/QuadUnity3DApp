using Assets.MonoBehaviour_Extensions.ViewSystem.ModeControl;
using Assets.MonoBehaviour_Extensions.ViewSystem.ModeViewController;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Controllers;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.ModeViewControl.Controllers;
using Assets.Services.ViewSystem.ModeViewControl.Interfaces;
using Assets.Services.ViewSystem.ViewControl.Controllers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MonoBehaviour_Extensions.ViewSystem.ViewController
{
    public class ViewCtrl : BaseViewController
    {
        private IModeViewController modeViewController;

        private IModeTrackingUpdateMode modeTrackingUpdate;

        private IModeTrackingUpdateModeRegister modeTrackingUpdateRegister;

        void Awake()
        {
            //Get Mode tracking object and register to update. 
            var modeTrackingUpdate = GameObject
                .FindObjectOfType<ModeController>()
                .GetComponent<ModeTrackingController>();

            this.modeTrackingUpdate = modeTrackingUpdate as IModeTrackingUpdateMode;
            this.modeTrackingUpdateRegister = modeTrackingUpdate as IModeTrackingUpdateModeRegister;

            //Get ModeViewController and register
            this.modeViewController = GameObject
                .FindObjectOfType<ModeViewCtrl>()
                .GetComponent<ModeViewCtrl>() as IModeViewController;

            //Build the mode mappings
            this.NextMode = new Dictionary<Modes, ModeMapping>
            {
                {Modes.Initialise, new ModeMapping(Modes.Initialise, Modes.SelectQuad) },
                {Modes.SelectQuad, new ModeMapping(Modes.SelectQuad,Modes.ConfigureQuad) },
                {Modes.ConfigureQuad, new ModeMapping(Modes.SelectQuad, Modes.CalibrateTestQuad) },
                {Modes.MissionPlan, new ModeMapping(Modes.ConfigureQuad, Modes.Execute) },
                {Modes.Execute, new ModeMapping(Modes.MissionPlan, Modes.MissionPlan) }
            };

            //Register
            this.modeViewController.RegisterToControlView(this);
            this.modeTrackingUpdateRegister.RegisterToUpdate(this);
        }

        void Update()
        {
            //Update diaplyed view based on current mode and mode status.
            this.UpdateDisplayViewForMode(modeTrackingUpdate, modeViewController);
        }
    }
}
