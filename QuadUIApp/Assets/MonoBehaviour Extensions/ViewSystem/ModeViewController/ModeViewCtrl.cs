using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Initialise;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Dynamic.Select;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Menu;
using Assets.MonoBehaviour_Extensions.ViewSystem.Views.Fixed.Status_Feeds;
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

            //Build List of dynamic views to control;
            this.modeDynamicViewMappings = new Dictionary<Modes, IView>
            {
                {Modes.SelectQuad,  FindObjectOfType<SelectActiveQuadView>().GetComponent<SelectActiveQuadView>() as IView }
            };

            //Build list of fixed views to control.
            this.fixedViewsMappings = new Dictionary<FixedViews, IView>
            {
                { FixedViews.Menu, FindObjectOfType<MenuView>().GetComponent<MenuView>() as IView },
                {FixedViews.StatusFeed,  FindObjectOfType<StatusFeedsView>().GetComponent<StatusFeedsView>() as IView}
            };
        }
    }
}
