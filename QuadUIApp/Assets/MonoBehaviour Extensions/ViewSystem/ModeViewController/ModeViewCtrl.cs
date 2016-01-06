using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Initialise;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Select;
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
            //Build List of views to control;
            modeUIMapping = new Dictionary<Modes, IView>
            {
                {Modes.Initialise,  FindObjectOfType<InitialiseView>().GetComponent<InitialiseView>() as IView },
                {Modes.SelectQuad,  FindObjectOfType<SelectActiveQuadView>().GetComponent<SelectActiveQuadView>() as IView }
            };
        }
    }
}
