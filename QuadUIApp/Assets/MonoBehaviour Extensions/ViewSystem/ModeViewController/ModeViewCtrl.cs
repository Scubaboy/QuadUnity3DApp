using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Initialise;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Select;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Menu;
using Assets.Services.ViewSystem.ModeControl;
using Assets.Services.ViewSystem.ModeViewControl.Controllers;
using Assets.Services.ViewSystem.View.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.MonoBehaviour_Extensions.ViewSystem.ModeViewController
{
    public class ModeViewCtrl : BaseModeViewController
    {
        void Awake()
        {
            var test = FindObjectOfType<SelectActiveQuadView>();

            //Build List of views to control;
            this.modeUIMapping = new Dictionary<Modes, IView>
            {
                {Modes.Initialise,  FindObjectOfType<MenuView>().GetComponent<MenuView>() as IView },
                {Modes.SelectQuad,  FindObjectOfType<SelectActiveQuadView>().GetComponent<SelectActiveQuadView>() as IView }
            };
        }
    }
}
