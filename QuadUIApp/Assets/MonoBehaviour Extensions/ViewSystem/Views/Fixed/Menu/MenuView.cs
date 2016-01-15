using Assets.MonoBehaviour_Extensions.ViewSystem.ViewController;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeControl.Controllers;
using Assets.Services.ViewSystem.ModeControl.Interfaces;
using Assets.Services.ViewSystem.View.Controller;
using Assets.Services.ViewSystem.ViewControl.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Menu
{
    public class MenuView : BaseViewController
    {
        public Button SelectQuad;
        public Button CalibrateSelectedQuad;
        public Button ConfigureSelectedQuad;
        public Button ConfigureMissionPlan;
        public Button ExecuteMissionPlan;

        public Text stuff;
        private IViewControllerRegister viewControllerRegister;
        private IViewControllerRequests viewControllerRequests;
        private IModeTracking currentMode;

        Dictionary<Modes, Action> modeToMenuOptionMappins;

        public override void Awake()
        {
               
            base.Awake();

            //Setup menu to mode mappings
            this.ConfigureModesToMenuOptions();

            this.currentMode = FindObjectOfType<ModeTrackingController>()
                 .GetComponent<ModeTrackingController>() as IModeTracking;

            this.viewControllerRegister = FindObjectOfType<ViewCtrl>()
                 .GetComponent<ViewCtrl>() as IViewControllerRegister;

            this.viewControllerRequests = FindObjectOfType<ViewCtrl>()
                 .GetComponent<ViewCtrl>() as IViewControllerRequests;

            this.viewControllerRegister.RegisterToRequestModeChange(this);

            //Configure button click listeners.
            this.ConfigureButtons();

        }

        void Update()
        {
            this.modeToMenuOptionMappins[this.currentMode.ActiveMode].Invoke();
        }

        private void ConfigureButtons()
        {
            this.SelectQuad.onClick.RemoveAllListeners();
            this.SelectQuad.GetComponentInChildren<Text>().text = "Select Quad";
            this.SelectQuad.onClick.AddListener(() => 
            {
                this.viewControllerRequests.RequestModeChange(Modes.SelectQuad, this);
            });

            this.ConfigureSelectedQuad.GetComponentInChildren<Text>().text = "Configure Selected Quad";
            this.ConfigureSelectedQuad.onClick.RemoveAllListeners();
            this.ConfigureSelectedQuad.onClick.AddListener(() =>
            {
                this.viewControllerRequests.RequestModeChange(Modes.ConfigureQuad, this);
            });

            this.CalibrateSelectedQuad.GetComponentInChildren<Text>().text = "Calibrate Selected Quad";
            this.CalibrateSelectedQuad.onClick.RemoveAllListeners();
            this.CalibrateSelectedQuad.onClick.AddListener(() =>
            {
                this.viewControllerRequests.RequestModeChange(Modes.CalibrateTestQuad, this);
            });

            this.ConfigureMissionPlan.GetComponentInChildren<Text>().text = "Configure Mission Plan";
            this.ConfigureMissionPlan.onClick.RemoveAllListeners();
            this.ConfigureMissionPlan.onClick.AddListener(() =>
            {
                this.viewControllerRequests.RequestModeChange(Modes.MissionPlan, this);
            });

            this.ExecuteMissionPlan.GetComponentInChildren<Text>().text = "Execute Mission Plan";
            this.ExecuteMissionPlan.onClick.RemoveAllListeners();
            this.ExecuteMissionPlan.onClick.AddListener(() =>
            {
                this.viewControllerRequests.RequestModeChange(Modes.Execute, this);
            });
        }

        protected override void ConfigureView()
        {
           
        }

        private void ConfigureModesToMenuOptions()
        {
            this.modeToMenuOptionMappins = new Dictionary<Modes, Action>
            {
                {Modes.Initialise, () =>
                    {
                        this.SelectQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = false;
                        this.ConfigureMissionPlan.interactable = false;
                        this.ExecuteMissionPlan.interactable = false;
                        this.CalibrateSelectedQuad.interactable = false;
                    }
                },
                {Modes.SelectQuad, () =>
                    {
                        this.SelectQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = false;
                        this.ConfigureMissionPlan.interactable = false;
                        this.ExecuteMissionPlan.interactable = false;
                        this.CalibrateSelectedQuad.interactable = false;
                    }
                },
                {Modes.CalibrateTestQuad, () =>
                    {
                       this.SelectQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = false;
                        this.ConfigureMissionPlan.interactable = false;
                        this.ExecuteMissionPlan.interactable = false;
                        this.CalibrateSelectedQuad.interactable = true;
                    }
                },
                {Modes.ConfigureQuad, () =>
                    {
                        this.SelectQuad.interactable = true;
                        this.CalibrateSelectedQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = true;
                        this.ConfigureMissionPlan.interactable = false;
                        this.ExecuteMissionPlan.interactable = false;

                    }
                },
                {Modes.MissionPlan, () =>
                    {
                        this.SelectQuad.interactable = true;
                        this.CalibrateSelectedQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = true;
                        this.ConfigureMissionPlan.interactable = true;
                        this.ExecuteMissionPlan.interactable = false;
                    }
                },
                {Modes.Execute, () =>
                    {
                        this.SelectQuad.interactable = true;
                        this.CalibrateSelectedQuad.interactable = true;
                        this.ConfigureSelectedQuad.interactable = true;
                        this.ConfigureMissionPlan.interactable = true;
                        this.ExecuteMissionPlan.interactable = true;
                    }
                }
            };
        }
    }
}
